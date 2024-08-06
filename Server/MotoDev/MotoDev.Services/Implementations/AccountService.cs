using EngineExpert.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Core.Dtos;
using MotoDev.Data;
using MotoDev.Services.Interaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Services.Implementations
{
    public class AccountService(IConfiguration configuration,
        IEmailService emailService,
        MotoDevDbContext dbContext) : IAccountService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly MotoDevDbContext _dbContext = dbContext;
        
        public async Task<BaseResponseModel> LoginAsync(LoginRequest request)
        {
            var username = request.Username;
            var password = request.Password;

            var user = await _dbContext.Users.Where(x =>
                     (x.Username == username || x.Email == username)
                     && x.Password == GetHashString(password)
                     && x.IsActive)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.NameIdentifier, username),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                foreach (var userRole in user.UserRoles)
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole.Role.Name));

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new BaseResponseModel
                {
                    IsOk = true,
                    Message = tokenHandler.WriteToken(token),
                };
            }
            else
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = "Invalid username/email or password"
                };
            }
        }

        public async Task<BaseResponseModel> RegisterAsync(RegisterAccountRequest request)
        {
            var doesExist = await _dbContext.Users.AnyAsync(x => x.Username == request.Email
                    || x.Email == request.Email);
            if (doesExist)
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = "A user with the provided username/email already exists"
                };

            if (request.Username.Length < 8 || !request.Username.Any(char.IsLetter))
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = "Username/email should be at least 8 characters long and should contain letters"
                };
            }

            if (!IsValidEmail(request.Email))
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = "Invalid email provided"
                };
            }

            if (request.Password.Length < 8 || !request.Password.Any(char.IsLetter))
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = "Password should be at least 8 characters long and should contain letters"
                };
            }

            var bytes = new byte[16];
            bytes = RandomNumberGenerator.GetBytes(16);
            var randomHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            await _dbContext.AddAsync(new User
            {
                Username = request.Username,
                Password = GetHashString(request.Password),
                Email = request.Email,
                CreatedAt = DateTime.Now,
                ResetPasswordToken = randomHash,
                IsActive = false,
            });

            await _dbContext.SaveChangesAsync();

            var message = $"Please click here to confirm your account -> http://www.engineexpert.com/ConfirmAccount/{randomHash}";
            await _emailService.SendEmailAsync(request.Email, message);

            return new BaseResponseModel
            {
                IsOk = true,
                Message = message,
            };
        }

        public async Task<BaseResponseModel> ConfirmAccountAsync(ConfirmAccountRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == request.ConfirmHash);
                if (user == null)
                {
                    return new BaseResponseModel
                    {
                        IsOk = false,
                        Message = $"The provided token is invalid"
                    };
                }

                user.IsActive = true;
                user.ResetPasswordToken = null;
                await _dbContext.SaveChangesAsync();

                return new BaseResponseModel
                {
                    IsOk = true,
                    Message = "Account is activated successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = $"An error occurred while activating your account"
                };
            }
        }

        public async Task<BaseResponseModel> ForgottenPasswordAsync(ForgottenEmailRequest request)
        {
            try
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.RecipientEmail);
                if (user == null)
                {
                    return new BaseResponseModel
                    {
                        IsOk = true,
                        Message = $"If such a user exists, a password reset email will be send"
                    };
                }

                var bytes = new byte[16];
                bytes = RandomNumberGenerator.GetBytes(16);
                var randomHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

                user.ResetPasswordToken = randomHash;
                await _dbContext.SaveChangesAsync();

                var message = $"Please click here to reset your password -> http://www.engineexpert.com/ResetPassword/{randomHash}";
                var isSent = await _emailService.SendEmailAsync(request.RecipientEmail, message);
                if (!isSent.IsOk)
                {
                    return new BaseResponseModel
                    {
                        IsOk = false,
                        Message = $"An error occurred while sending a reset password link"
                    };
                }

                return new BaseResponseModel
                {
                    IsOk = true,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = $"An error occurred while sending a reset password link"
                };
            }
        }

        public async Task<BaseResponseModel> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == request.ResetPasswordToken);
                if (user == null)
                {
                    return new BaseResponseModel
                    {
                        IsOk = false,
                        Message = $"The provided token is invalid"
                    };
                }

                if (request.Password.Length < 8 || !request.Password.Any(char.IsLetter))
                {
                    return new BaseResponseModel
                    {
                        IsOk = false,
                        Message = "Password should be at least 8 characters long and should contain letters"
                    };
                }

                user.Password = GetHashString(request.Password);
                user.ResetPasswordToken = null;
                await _dbContext.SaveChangesAsync();

                return new BaseResponseModel
                {
                    IsOk = true,
                    Message = "Password is changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = $"An error occurred while changing the password"
                };
            }
        }

        private byte[] GetHash(string inputString)
        {
            using (var algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                stringBuilder.Append(b.ToString("X2"));

            return stringBuilder.ToString().ToLowerInvariant();
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}