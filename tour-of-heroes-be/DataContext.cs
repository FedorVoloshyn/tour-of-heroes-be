using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;
using tour_of_heroes_be.Repositories;

namespace tour_of_heroes_be
{
    internal class DataContext : IDataContext
    {
        readonly DbContext _dbContext;
        readonly Lazy<BaseRepository<Hero>> _lazyHeroesRepository;

        public DataContext(DbContext dbContext)
        {
            _dbContext = dbContext;

            _lazyHeroesRepository = new Lazy<BaseRepository<Hero>>(() => new BaseRepository<Hero>(_dbContext));
        }

        public void Dispose() =>
            _dbContext.Dispose();

        public IBaseRepository<Hero> Heroes => _lazyHeroesRepository.Value;

        public void Save() =>
            _dbContext.SaveChanges();

        public async Task SaveAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.SaveChangesAsync(cancellationToken);
    }
}