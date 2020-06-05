using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourOfHeroes.Models;

namespace TourOfHeroes.Services
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
