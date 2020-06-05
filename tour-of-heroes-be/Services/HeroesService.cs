using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourOfHeroes.Models;

namespace TourOfHeroes.Services
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

        public async Task<int> AddHeroAsync(Hero hero)
        {
            using (var dataContext = DataContextFactory.CreateContext())
            {
                dataContext.Heroes.Add(hero);

                await dataContext.SaveAsync();
            }

            return hero.Id;
        }

        public async Task DeleteHeroAsync(int id)
        {
            using (var dataContext = DataContextFactory.CreateContext())
            {
                var hero = (await dataContext.Heroes.GetAsync(h => h.Id == id)).SingleOrDefault();
                if (hero == null)
                    throw new Exception($"Hero {id} not found!");

                dataContext.Heroes.Delete(hero);

                await dataContext.SaveAsync();
            }
        }
    }
}
