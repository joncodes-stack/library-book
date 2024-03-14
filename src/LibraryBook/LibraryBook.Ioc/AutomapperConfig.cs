using AutoMapper;
using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Entities;
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
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
