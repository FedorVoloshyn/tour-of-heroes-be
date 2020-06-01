using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tour_of_heroes_be.Models;
using tour_of_heroes_be.Repositories;

namespace tour_of_heroes_be
{
    public class TourOfHeroesContext : DbContext
    {
        public TourOfHeroesContext(DbContextOptions options)
            : base(options)
        {
            System.Diagnostics.Debug.WriteLine($"Create '{nameof(TourOfHeroesContext)}' instance: {GetHashCode()}");
        }

        public DbSet<Hero> Heroes { get; set; }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            var entities = ChangeTracker.Entries();
            foreach (var entry in entities)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["Removed"] = null;
                        break;
                    case EntityState.Deleted:
                        var check = entry.IsKeySet;
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["Removed"] = DateTime.UtcNow;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddSoftDeletion();

            modelBuilder
                .Entity<Hero>()
                .HasIndex("Name", "Removed")
                .HasFilter(null)
                .IsUnique();
        }
    }
}
