using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface.Repository;
using LibraryBook.Domain.Interface;
using LibraryBook.Domain.Interface.Service;
using LibraryBook.Domain.Services;
using LibraryBook.Domain.Interface.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Interface.Repository;

namespace LibraryBook.Application.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(INotificador notificador,
        IBookRepository bookRepository) : base(notificador)
        {
            _bookRepository = bookRepository;
        }

        public async Task Add(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task Update(Book book)
        {
            await _bookRepository.Update(book);
        }

        public async Task Delete(Guid id)
        {
            await _bookRepository.Delete(id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
             return await _bookRepository.GetAll();
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<IEnumerable<Book>> GetByUser(Guid idUser)
        {
            return await _bookRepository.GetBookByUser(idUser);
        }

        public void Dispose()
        {
            _bookRepository.Dispose();
        }
    }
}
