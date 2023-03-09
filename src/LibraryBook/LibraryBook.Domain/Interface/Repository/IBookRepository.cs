using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Interface.Repository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> GetBookByUser(Guid idUser);
    }
}
