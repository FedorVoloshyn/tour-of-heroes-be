using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;

namespace tour_of_heroes_be.Services
{
    public interface IHeroesService
    {
        Task<IEnumerable<Hero>> GetHeroesAsync();
        Task<Hero> GetHeroAsync(int id);
        Task PutHeroAsync(Hero hero);
        Task<int> AddHeroAsync(Hero hero);
        Task DeleteHeroAsync(int id);
    }
}
