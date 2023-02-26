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
    public class UserMap : BaseMap<User>
    {
        public UserMap() : base("tb_users") { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FullName).HasColumnName("fullName").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(50).IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnName("phoneNumber").HasMaxLength(12).IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(500).IsRequired();
            builder.Property(x => x.ProfilePic).HasColumnName("profilePic");
            builder.Property(x => x.Active).HasColumnName("active");
            builder.Property(x => x.Code).HasColumnName("code");
        }
    }
}
