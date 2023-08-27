using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface;
using LibraryBook.Domain.Interface.Repository;
using LibraryBook.Domain.Interface.Service;
using LibraryBook.Domain.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Services
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

        public async Task Update(User user)
        {
            await _userRepository.Update(user);
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }

        public async Task<LoginResponseDto> GenerateToken(User user)
        {
            try
            {
                var secretKey = _configuration.GetSection("JWT:Secret");
                var Issuer = _configuration.GetSection("JWT:Issuer");
                var validOn = _configuration.GetSection("JWT:ValidOn");

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

                var refreshToken = GenerateRefreshToken();

                var response = new LoginResponseDto
                {
                    AccessToken = encodedToken,
                    RefreshToken = refreshToken,
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

        public string GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);

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

        public async Task<User> GetUserByCode(int code)
        {
            return await _userRepository.GetUserByCode(code);
        }
    }
}

