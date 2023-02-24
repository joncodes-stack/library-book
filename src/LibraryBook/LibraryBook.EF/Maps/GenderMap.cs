using LibraryBook.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Maps
{
    public class GenderMap : BaseMap<Gender>
    {
        public GenderMap() : base("tb_gender") {}

        public override void Configure(EntityTypeBuilder<Gender> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Active).IsRequired();
        }
    }
}
