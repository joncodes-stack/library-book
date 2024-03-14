using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class Gender : BaseEntity
    {
        public Gender()
        {
                
        }

        public Gender(string name, bool active)
        {
            Name = name;
            Active = active;
        }

        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
