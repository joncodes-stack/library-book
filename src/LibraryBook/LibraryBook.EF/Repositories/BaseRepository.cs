using LibraryBook.Domain.Entities;
using LibraryBook.Domain.Interface.Repository;
using LibraryBook.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.EF.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly LibraryBookContext _libraryBook;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(LibraryBookContext libraryBook)
        {
            _libraryBook = libraryBook;
            DbSet = libraryBook.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _libraryBook.SaveChangesAsync();
        }

        public void Dispose()
        {
            _libraryBook?.Dispose();
        }
    }
}
