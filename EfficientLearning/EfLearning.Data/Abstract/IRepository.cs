using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T t);
        Task<int> CountAsync();
        void Delete(T entity);
        void Dispose();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> CommitAsync();
        Task<T> UpdateAsync(T t, object key);
    }
}
