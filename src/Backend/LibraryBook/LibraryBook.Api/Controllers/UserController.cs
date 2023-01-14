using AutoMapper;
using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Service;
using LibraryBook.CrossCutting.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;


namespace LibraryBook.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserController(INotificador notificador,
                              IMapper mapper,
                              IUserService userService,
                              IEmailService emailService) : base(notificador) 
        {
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUserDto register)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _mapper.Map<User>(register);

            user.Password = BC.HashPassword(register.Password);

            await _userService.Add(user);

            return CustomResponse("Usuário Cadastrado com sucesso");
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            await _emailService.SendEmailAsync(email);

            return CustomResponse("Email Enviado com sucesso");
        }
    }
}
