using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public abstract class BaseRepository<T> : IRepository<T>
      where T : class
    {
        protected EfContext _context;

        public BaseRepository(EfContext context)
        {
            _context = context;
        }
        public virtual async Task<ICollection<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task<T> AddAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            return t;

        }
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

        }
        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);

            }
            return exist;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }


        public async virtual Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }



        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


        public void DeleteById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }




    }
}
