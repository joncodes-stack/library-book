using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Dtos.BooksDto
{
    public class BookDto
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }

        public long? IsbnNumber { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Author { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Editor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Synopsis { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public IFormFile PictureBook { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdGender { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdUser { get; set; }
    }
}
