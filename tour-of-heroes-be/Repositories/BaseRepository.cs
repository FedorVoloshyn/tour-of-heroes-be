using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace tour_of_heroes_be.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; }

        public virtual IQueryable<T> All =>
            DbContext.Set<T>();

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null) =>
            await DbContext.Set<T>()
            .AnyAsync(predicate ?? (r => true));

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null) =>
            await DbContext.Set<T>()
            .CountAsync(predicate ?? (r => true));

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null) =>
            await DbContext.Set<T>()
            .Where(predicate ?? (r => true))
            .ToArrayAsync();

        public virtual async Task<IEnumerable<T>> GetAsync(int take, Expression<Func<T, bool>> predicate = null, int skip = 0, Expression<Func<T, object>> sortBy = null)
        {
            var query = DbContext.Set<T>()
                .Where(predicate ?? (r => true));

            query = query.Skip(skip).Take(take);

            return await query.ToArrayAsync();
        }

        public virtual async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null) =>
            await DbContext.Set<T>()
            .Where(predicate ?? (r => true))
            .Select(selector)
            .ToArrayAsync();

        public virtual void Add(T obj) =>
            DbContext.Add(obj);

        public virtual void Update(T obj) =>
            DbContext.Update(obj);

        public virtual void Delete(T obj) =>
            DbContext.Remove(obj);
    }
}
