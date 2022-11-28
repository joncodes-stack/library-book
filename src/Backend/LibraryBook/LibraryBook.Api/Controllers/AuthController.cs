using LibraryBook.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;
        public AuthController(INotificador notificador, ITokenService tokenService) : base(notificador)
        {
            _tokenService = tokenService;
        }

        [HttpPost("v1/login")]
        public async Task<ActionResult> Login() 
        {
            var token = _tokenService.GenerateToken(null);

            return CustomResponse(token);
        }
    }
}
