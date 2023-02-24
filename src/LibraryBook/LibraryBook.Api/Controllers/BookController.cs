using LibraryBook.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : BaseController
    {
        public BookController(INotificador notificador) :base(notificador) {}
    }
}
