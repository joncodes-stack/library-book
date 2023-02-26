using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface.Repository;
using LibraryBook.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryBookContext libraryBook) : base(libraryBook) { }

        public async Task<IEnumerable<Book>> GetBookByUser(Guid idUser)
        {
            return await _libraryBook.Book
                   .Where(x => x.IdUser == idUser).ToListAsync();
        }

    }
}
