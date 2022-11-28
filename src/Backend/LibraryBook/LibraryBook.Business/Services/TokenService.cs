using LibraryBook.Business.Configuration;
using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> GenerateToken(User user)
        {
			try
			{
                var secretKey = _configuration.GetSection("Settings:Secret");
                var Issuer = _configuration.GetSection("Settings:Issuer");
                var validOn = _configuration.GetSection("Settings:ValidOn");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey.Value);
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = Issuer.Value,
                    Audience = validOn.Value,
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                });

                var encodedToken = tokenHandler.WriteToken(token);

                var response = new LoginResponseDto
                {
                    AccessToken = encodedToken,
                    ExpiresIn = TimeSpan.FromHours(8).TotalSeconds,
                    UserToken = new UserTokenDto
                    {
                        Id = user.Id.ToString(),
                        Email = user.Email,
                    }
                };

                return response;

            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
