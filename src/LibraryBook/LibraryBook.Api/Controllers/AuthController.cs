using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Service;
using LibraryBook.CrossCutting.Interfaces;
using LibraryBook.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace LibraryBook.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(INotificador notificador, 
                              IUserService userService,
                              IEmailService emailsService,
                              IConfiguration configuration) : base(notificador)
        {
            _userService = userService;
            _emailService = emailsService;
            _configuration = configuration;
        }

        [HttpPost("v1/login")]
        public async Task<ActionResult> Login(LoginUserDto login) 
        {
            var user = await _userService.GetUserByEmail(login.Email);

            if (user.Active == false)
            {
                return CustomResponse($@"O Email {login.Email} não foi validado");
            }

            if (user == null || !BC.Verify(login.Password, user.Password))
            {
                return BadRequest(new {message = "Email or password is incorrect" });
            }

            var refreshToken = _userService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            await _userService.Update(user);


            var loginResponse = await _userService.GenerateToken(user);

            return CustomResponse(loginResponse);
        }

        [HttpPost("v1/forget-password")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound("Usuário não encontrado");

            var random = new Random();
            var code = random.Next(100000, 999999);
            user.Code = code;

            await _userService.Update(user);

            await _emailService.SendEmailForgetAsync(email, code);

            return CustomResponse("O email com o codigo de recuperação foi enviado com sucesso.");
        }

        [HttpPut("v1/validate-code")]
        public async Task<ActionResult> ValidateCode(string email ,int code)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound("Usuário não encontrado");

            if (user.Code == code)
            {
                return CustomResponse("Codigo validado com sucesso.");
            } else
            {
                return CustomResponse("Codigo inválido, favor verificar o codigo enviado para seu email.");
            }
        }


        [HttpPut("v1/change-password")]
        public async Task<ActionResult> ChangePassword(string email, string password)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound("Usuário não encontrado");

            user.Password = password;

            await _userService.Update(user);

            return CustomResponse("Senha atualizada com sucesso.");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDto tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            string username = principal.Identity.Name;
            var user = await _userService.GetUserByEmail(username);

            if (user == null || user.RefreshToken != refreshToken ||
                       user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var newAccessToken = await _userService.GenerateToken(user);
            var newRefreshToken = _userService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userService.Update(user);

            return new ObjectResult(new
            {
                accessToken = newAccessToken.AccessToken,
                refreshToken = newRefreshToken
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                   .GetBytes(_configuration["JWT:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();


            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                            out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                      !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                     StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
