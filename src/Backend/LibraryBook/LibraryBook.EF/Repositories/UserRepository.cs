using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface.Repository;
using LibraryBook.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryBookContext libraryBook) : base(libraryBook) {}

        public async Task<User> GetUserByEmail(string email)
        {
            return await _libraryBook.User.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
