using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Service;
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
        public AuthController(INotificador notificador, 
                              IUserService userService) : base(notificador)
        {
            _userService = userService;
        }

        [HttpPost("v1/login")]
        public async Task<ActionResult> Login(LoginUserDto login) 
        {
            var user = await _userService.GetUserByEmail(login.Email);

            if(user == null || !BC.Verify(login.Password, user.Password))
            {
                CustomResponse("Email or password is incorrect");
            } 

            var loginResponse = await _userService.GenerateToken(user);

            return CustomResponse(loginResponse);
        }

        [HttpGet("v1/forget-password")]
        public ActionResult ForgetPassword()
        {
            return Ok("teste");
        }
    }
}
