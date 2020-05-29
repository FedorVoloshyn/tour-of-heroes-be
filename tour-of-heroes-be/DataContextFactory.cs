using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using tour_of_heroes_be.Models;

namespace tour_of_heroes_be
{
    internal class DataContextFactory : IDataContextFactory
    {
        public DataContextFactory(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            ConnectionString = configuration.GetValue("Database", "ConnectionString");
        }

        protected string ConnectionString { get; }

        public IDataContext CreateContext()
        {
            var dbContext = CreateDbContext();
            
            return new DataContext(dbContext);
        }

        protected internal virtual DbContext CreateDbContext()
        {
            var heroesDbContext = new TourOfHeroesContext(CreateDbContextOptions());
            heroesDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return heroesDbContext;
        }

        protected virtual DbContextOptions CreateDbContextOptions() =>
            new DbContextOptionsBuilder<TourOfHeroesContext>()
            .UseSqlServer(CreateDbConnection())
            .Options;

        protected virtual DbConnection CreateDbConnection() =>
            new SqlConnection(ConnectionString);
    }
}
