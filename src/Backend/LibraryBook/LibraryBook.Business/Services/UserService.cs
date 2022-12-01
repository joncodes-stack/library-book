using LibraryBook.Business.Dtos.Responses;
using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Repository;
using LibraryBook.Business.Interface.Service;
using LibraryBook.Business.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(INotificador notificador, 
                           IUserRepository userRepository,
                           IConfiguration configuration) : base(notificador)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task Add(User user)
        {
            if (!ExecutarValidacao(new UserValidation(), user)) return;

            await _userRepository.Add(user);
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepository.Dispose();
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
                var token =  tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = Issuer.Value,
                    Audience = validOn.Value,
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                });

                var encodedToken =  tokenHandler.WriteToken(token);

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

                return  response;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
    }
}
