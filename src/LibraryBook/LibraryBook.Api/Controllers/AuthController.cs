using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface;
using LibraryBook.Domain.Interface.Service;
using LibraryBook.CrossCutting.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace LibraryBook.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AuthController(INotificador notificador, 
                              IUserService userService,
                              IEmailService emailsService) : base(notificador)
        {
            _userService = userService;
            _emailService = emailsService;
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
    }
}
