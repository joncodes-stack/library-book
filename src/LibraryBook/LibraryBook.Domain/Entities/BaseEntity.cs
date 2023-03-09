using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Created_At= DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }

    }
}
