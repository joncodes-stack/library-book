using AutoMapper;
using LibraryBook.Domain.Interface;
using LibraryBook.Domain.Interface.Service;
using LibraryBook.CrossCutting.Interfaces;
using LibraryBook.Domain.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryBook.Domain.Entities;
using LibraryBook.CrossCutting.Services;
using LibraryBook.Domain.Services;
using Microsoft.Win32;
using LibraryBook.Domain.Dtos.BooksDto;

namespace LibraryBook.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(INotificador notificador,
                              IBookService bookService,
                              IMapper mapper) :base(notificador) 
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("get-book-by-user")]
        public async Task<IEnumerable<BookDto>> GetBooksByUser(Guid idUser)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _bookService.GetByUser(idUser));
        }

        [HttpPost("add-book")]
        public async Task<ActionResult> AddBook([FromBody]BookDto bookDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var book = _mapper.Map<Book>(bookDto);

            await _bookService.Add(book);

            return CustomResponse("Dados do livro cadastrado com sucesso");
        }

        [HttpPut("update-book")]
        public async Task<ActionResult> UpdateBook(UpdateBookDto bookDto)
        {
            if (bookDto.Id == null)
            {
                return BadRequest("Id do livro e obrigatório");
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var book = await _bookService.GetById(bookDto.Id);
            
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Synopsis = bookDto.Synopsis;
            book.IsbnNumber = bookDto.IsbnNumber;
            book.IdGender = bookDto.IdGender;
            book.Editor = bookDto.Editor;

            await _bookService.Update(book);

            return CustomResponse("Dados do livro Atualizado com sucesso!");
        }

        [HttpDelete("remove-book")]
        public async Task<ActionResult> RemoveBook(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id do livro e obrigatório");
            }

            var book = await _bookService.GetById(id);

            await _bookService.Delete(book.Id);

            return CustomResponse("Dados do livro Atualizado com sucesso!");
        }
    }
}
