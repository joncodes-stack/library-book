using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Entities
{
    public class Books : BaseEntity
    {
        public string Title { get; set; }
        public string IsbnNumber { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
        public string Synopsis { get; set; }
        public string PictureBook { get; set; }
        public int IdGender { get; set; }
        public Gender Gender { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }

    }
}
