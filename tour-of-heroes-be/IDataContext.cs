using System.Threading.Tasks;
using tour_of_heroes_be.Repositories;
using tour_of_heroes_be.Models;
using System;

namespace tour_of_heroes_be
{
    internal interface IDataContext : IDisposable
    {
        IBaseRepository<Hero> Heroes { get; }
        void Save();
        Task SaveAsync();
    }
}
