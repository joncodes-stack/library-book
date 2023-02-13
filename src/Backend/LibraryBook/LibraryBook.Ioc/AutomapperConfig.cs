using AutoMapper;
using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Ioc
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
