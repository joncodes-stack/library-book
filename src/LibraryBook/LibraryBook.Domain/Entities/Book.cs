using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public long? IsbnNumber { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
        public string Synopsis { get; set; }
        public string PictureBook { get; set; }

        //EF
        public Guid IdGender { get; set; }
        public virtual Gender Gender { get; set; }
        public Guid IdUser { get; set; }
        public virtual User User { get; set; }

    }
}
