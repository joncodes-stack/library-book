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

            var getUser = await _userService.GetUserByEmail(register.Email);

            if (getUser != null)
            {
                return CustomResponse("Email já cadastrado");
            }
            
            var user = _mapper.Map<User>(register);

            var random = new Random();
            var code = random.Next(100000, 999999);

            user.Password = BC.HashPassword(register.Password);
            user.Code = code;

            await _userService.Add(user);

            await _emailService.SendValidatemEmailAsync(register.Email,register.FullName, code);

            return CustomResponse("Usuário Cadastrado com sucesso, favor verifique sua caixa de email para confirmação !!");
        }

        [HttpPost("active-account")]
        public async Task<ActionResult> ActiveAccount(int code)
        {
            var user = await _userService.GetUserByCode(code);

            user.Active = true;

            await _userService.Update(user);

            return CustomResponse("Email Validado com sucesso !!");
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
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound("Usuário não encontrado");

            var random = new Random();
            var code = random.Next(100000, 999999);
            user.Code = code;

            await _userService.Update(user);

            await _emailService.SendEmailForgetAsync(email, user.FullName, code);

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
