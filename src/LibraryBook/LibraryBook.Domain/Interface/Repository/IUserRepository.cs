using LibraryBook.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCode(int code);
    }
}
