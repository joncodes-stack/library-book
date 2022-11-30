using AutoMapper;
using LibraryBook.Business.Dtos.Responses;
using LibraryBook.Business.Entities;

namespace LibraryBook.Api.Configuration
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
