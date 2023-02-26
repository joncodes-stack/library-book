using AutoMapper;
using LibraryBook.Domain.Interface;
using LibraryBook.Domain.Interface.Service;
using LibraryBook.CrossCutting.Interfaces;
using LibraryBook.Domain.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryBook.Domain.Entities;
using LibraryBook.CrossCutting.Services;
using LibraryBook.Domain.Dtos;
using LibraryBook.Domain.Services;
using Microsoft.Win32;

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

        [HttpPost("register")]
        public async Task<ActionResult> AddBook(BookDto bookDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var book = _mapper.Map<Book>(bookDto);

            await _bookService.Add(book);

            return CustomResponse("Livro cadastrado com sucesso");
        }

        [HttpPost("register")]
        public async Task<ActionResult> UpdateBook(BookDto bookDto)
        {
            if (bookDto.Id == null)
            {
                return BadRequest("Id do livro e obrigatório");
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var book = _mapper.Map<Book>(bookDto);

            await _bookService.Add(book);

            return CustomResponse("Livro cadastrado com sucesso");
        }

    }
}
