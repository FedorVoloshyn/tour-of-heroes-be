using System.Threading.Tasks;
using TourOfHeroes.Repositories;
using TourOfHeroes.Models;
using System;
using System.Threading;

namespace TourOfHeroes
{
    internal interface IDataContext : IDisposable
    {
        IBaseRepository<Hero> Heroes { get; }
        void Save();
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
