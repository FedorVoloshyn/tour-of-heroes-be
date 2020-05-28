using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;

namespace tour_of_heroes_be.Repositories
{
    public interface IHeroesRepository
    {
        Task<IEnumerable<Hero>> GetAllAsync(Expression<Func<Hero, bool>> predicate = null);
    }
}
