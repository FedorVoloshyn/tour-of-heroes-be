﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;

namespace tour_of_heroes_be.Repositories
{
    internal class HeroesRepository : BaseRepository<Hero>, IHeroesRepository
    {
        public HeroesRepository(DbContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<Hero>> GetAllAsync(Expression<Func<Hero, bool>> predicate = null) =>
            await DbContext.Set<Hero>()
            .ToArrayAsync();
    }
}
