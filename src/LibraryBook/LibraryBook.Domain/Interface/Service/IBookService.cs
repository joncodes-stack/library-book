using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Interface.Service
{
    public interface IBookService : IDisposable
    {
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(Guid id);
        Task<Book> GetById(Guid id);
        Task<IEnumerable<Book>> GetByUser(Guid idUser);
        Task<IEnumerable<Book>> GetAll();
    }
}
