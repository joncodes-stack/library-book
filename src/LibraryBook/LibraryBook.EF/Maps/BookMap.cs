using LibraryBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Maps
{
    public class BookMap : BaseMap<Book>
    {
        public BookMap() : base("tb_book") {}

        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(100).IsRequired();
            builder.Property(x => x.IsbnNumber).HasColumnName("isbnNumber").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Author).HasColumnName("auhor").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Editor).HasColumnName("editor").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Synopsis).HasColumnName("synopsis").HasMaxLength(100).IsRequired();
            builder.Property(x => x.PictureBook).HasColumnName("pictureBook").HasMaxLength(100).IsRequired();

            builder.Property(x => x.IdGender).HasColumnName("id_gender").IsRequired();
            builder.HasOne(x => x.Gender).WithMany().HasForeignKey(x => x.IdGender);

            builder.Property(x => x.IdUser).HasColumnName("id_user").IsRequired();
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.IdUser);
        }
    }
}
