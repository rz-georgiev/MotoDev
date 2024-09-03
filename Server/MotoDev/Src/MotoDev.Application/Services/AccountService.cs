using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Constants;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Application.Services
{
    public class AccountService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IAccountService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse> LoginAsync(LoginRequest request)
        {
            var username = request.Username;
            var password = request.Password;
            
            var user = await _dbContext.Users.Where(x =>
                     (x.Username == username || x.Email == username)
                     && x.Password == GetHashString(password)
                     && x.IsActive)
                .Include(x => x.Role)
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
                        new(CustomClaimTypes.UserId, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };

               
               tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new BaseResponse
                {
                    IsOk = true,
                    Message = tokenHandler.WriteToken(token),
                };
            }
            else
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "Invalid username/email or password"
                };
            }
        }

        public async Task<BaseResponse> RegisterAsync(RegisterAccountRequest request)
        {
            var doesExist = await _dbContext.Users.AnyAsync(x => x.Username == request.Email
                    || x.Email == request.Email);
            if (doesExist)
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "A user with the provided username/email already exists"
                };

            if (request.Email.Length < 8 || !request.Email.Any(char.IsLetter))
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "Username/email should be at least 8 characters long and should contain letters"
                };
            }

            if (!IsValidEmail(request.Email))
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "Invalid email provided"
                };
            }

            if (request.Password.Length < 8 || !request.Password.Any(char.IsLetter))
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "Password should be at least 8 characters long and should contain letters"
                };
            }

            var bytes = new byte[16];
            bytes = RandomNumberGenerator.GetBytes(16);
            var randomHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            var user = await _dbContext.AddAsync(new User
            {
                Username = request.Email,
                Password = GetHashString(request.Password),
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                RoleId = (int)RoleOption.Owner,
                ResetPasswordToken = randomHash,
                IsActive = false,
                CreatedByUserId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value)
            });

            await _dbContext.SaveChangesAsync();

            //var message = $"Please click here to confirm your account -> http://www.motodev.space/confirmAccount/{randomHash}";
            var message = $"Please click here to confirm your account -> http://localhost:4200/confirmAccount/{randomHash}";
            await _emailService.SendEmailAsync(request.Email, message);

            return new BaseResponse
            {
                IsOk = true,
                Message = message,
            };
        }

        public async Task<BaseResponse> ConfirmAccountAsync(ConfirmAccountRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == request.ConfirmHash);
                if (user == null)
                {
                    return new BaseResponse
                    {
                        IsOk = false,
                        Message = $"The provided token is invalid"
                    };
                }

                user.IsActive = true;
                user.ResetPasswordToken = null;
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    IsOk = true,
                    Message = "Account is activated successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = $"An error occurred while activating your account"
                };
            }
        }

        public async Task<BaseResponse> ForgottenPasswordAsync(ForgottenEmailRequest request)
        {
            try
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.RecipientEmail);
                if (user == null)
                {
                    return new BaseResponse
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

                //var message = $"Please click here to reset your password -> http://www.motodev.com/forgottenPasswordConfirm/{randomHash}";
                var message = $"Please click here to reset your password -> http://localhost:4200/forgottenPasswordConfirm/{randomHash}";
                var isSent = await _emailService.SendEmailAsync(request.RecipientEmail, message);
                if (!isSent.IsOk)
                {
                    return new BaseResponse
                    {
                        IsOk = false,
                        Message = $"An error occurred while sending a reset password link"
                    };
                }

                return new BaseResponse
                {
                    IsOk = true,
                    Message = "A password reset link is sent. Please check your email",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = $"An error occurred while sending a reset password link"
                };
            }
        }

        public async Task<BaseResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == request.ResetPasswordToken);
                if (user == null)
                {
                    return new BaseResponse
                    {
                        IsOk = false,
                        Message = $"The provided token is invalid"
                    };
                }

                if (request.Password.Length < 8 || !request.Password.Any(char.IsLetter))
                {
                    return new BaseResponse
                    {
                        IsOk = false,
                        Message = "Password should be at least 8 characters long and should contain letters"
                    };
                }

                user.Password = GetHashString(request.Password);
                user.ResetPasswordToken = null;
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    IsOk = true,
                    Message = "Password is changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
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