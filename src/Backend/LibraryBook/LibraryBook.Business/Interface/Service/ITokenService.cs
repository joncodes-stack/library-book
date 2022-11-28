using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Interface.Service
{
    public interface ITokenService
    {
        Task<LoginResponseDto> GenerateToken(User user);
    }
}
