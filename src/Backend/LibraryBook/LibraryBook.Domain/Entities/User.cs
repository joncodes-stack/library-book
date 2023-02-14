using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public bool Active { get; set; }
        public int? Code { get; set; }
    }
}
