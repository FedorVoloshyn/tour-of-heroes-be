using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TourOfHeroes.Models;

namespace TourOfHeroes.Repositories
{
    public interface IHeroesRepository
    {
        Task<IEnumerable<Hero>> GetAllAsync(Expression<Func<Hero, bool>> predicate = null);
    }
}
