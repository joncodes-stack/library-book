using LibraryBook.Business.Dtos;
using LibraryBook.Business.Dtos;
using LibraryBook.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Interface.Service
{
    public interface IUserService : IDisposable
    {
        Task<User> GetById(Guid id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid id);
        Task<LoginResponseDto> GenerateToken(User user);
        Task<User> GetUserByEmail(string email);
    }
}
