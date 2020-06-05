using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TourOfHeroes.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetAsync(int take, Expression<Func<T, bool>> predicate = null, int skip = 0, Expression<Func<T, object>> sortBy = null);
        Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
