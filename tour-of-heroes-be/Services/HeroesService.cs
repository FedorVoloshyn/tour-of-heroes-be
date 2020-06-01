using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;

namespace tour_of_heroes_be.Services
{
    internal class HeroesService : IHeroesService
    {
        public HeroesService(IDataContextFactory dataContextFactory)
        {
            DataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }
        IDataContextFactory DataContextFactory { get; }

        public async Task<IEnumerable<Hero>> GetHeroesAsync()
        {
            using (var dataContext = DataContextFactory.CreateContext())
                return await dataContext.Heroes
                    .GetAsync();
        }

        public async Task<Hero> GetHeroAsync(int id)
        {
            using (var dataContext = DataContextFactory.CreateContext())
                return (await dataContext.Heroes
                    .GetAsync(h => h.Id == id)).SingleOrDefault();
        }

        public async Task PutHeroAsync(Hero hero)
        {
            using (var dataContext = DataContextFactory.CreateContext())
            {
                dataContext.Heroes.Update(hero);

                await dataContext.SaveAsync();
            }
        }
    }
}
