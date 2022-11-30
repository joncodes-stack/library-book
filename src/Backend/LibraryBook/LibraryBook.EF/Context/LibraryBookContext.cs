using LibraryBook.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Context
{
    public class LibraryBookContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public LibraryBookContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public LibraryBookContext(DbContextOptions<LibraryBookContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}
