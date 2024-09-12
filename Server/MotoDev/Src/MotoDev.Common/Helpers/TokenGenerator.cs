using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Common.Constants;
using MotoDev.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotoDev.Common.Helpers
{
    public class TokenGenerator
    {
        private static IConfiguration _configuration;

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GenerateToken(User user, string imageUrl)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new(ClaimTypes.NameIdentifier, user.Username),
                        new(CustomClaimTypes.UserId, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            tokenDescriptor.Subject.AddClaim(new Claim(CustomClaimTypes.ImageUrl, imageUrl));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}