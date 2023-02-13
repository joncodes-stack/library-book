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

        [HttpPut("update-profile")]
        public async Task<ActionResult> UpdateProfile(UpdateUserDto updateProfile)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);



            var user = await _userService.GetById(updateProfile.Id);

            user.FullName = updateProfile.FullName;
            user.Email = updateProfile.Email;
            user.PhoneNumber = updateProfile.PhoneNumber;

            await _userService.Update(user);

            return CustomResponse("Perfil atualizado com sucesso");
        }

        [HttpPut("update-profile-pic")]
        public async Task<ActionResult> UpdateProfilePic(UpdateUserProfilePicDto updateProfile)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userService.GetById(updateProfile.Id);

            user.ProfilePic = updateProfile.ProfilePic;

            await _userService.Update(user);

            return CustomResponse("Perfil atualizado com sucesso");
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            await _emailService.SendEmailAsync(email);

            return CustomResponse("Email enviado com sucesso");
        }

        [HttpPost("confirm-password")]
        public async Task<ActionResult> ConfirmPassword(string email)
        {
            await _emailService.SendConfirmEmailAsync(email);

            return CustomResponse("Email de confirmação enviado com sucesso");
        }
    }
}
