using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Entities
{
    public class Gender : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
