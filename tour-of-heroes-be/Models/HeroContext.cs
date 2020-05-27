using Microsoft.EntityFrameworkCore;

namespace tour_of_heroes_be.Models
{
    public class HeroContext : DbContext
    {
        public HeroContext(DbContextOptions<HeroContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> TodoItems { get; set; }
    }
}
