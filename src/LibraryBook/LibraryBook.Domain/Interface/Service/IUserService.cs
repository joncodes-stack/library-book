using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Domain.Interface.Service
{
    public interface IUserService : IDisposable
    {
        Task<User> GetById(Guid id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid id);
        Task<LoginResponseDto> GenerateToken(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCode(int code);
    }
}
