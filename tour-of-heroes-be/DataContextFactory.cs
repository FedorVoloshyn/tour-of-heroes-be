using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TourOfHeroes.Models;

namespace TourOfHeroes
{
    internal class DataContextFactory : IDataContextFactory
    {
        public DataContextFactory(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            ConnectionString = configuration["Database:ConnectionString"];
        }

        protected string ConnectionString { get; }

        public IDataContext CreateContext()
        {
            var dbContext = CreateDbContext();
            
            return new DataContext(dbContext);
        }

        protected internal virtual DbContext CreateDbContext()
        {
            var contextOptions = CreateDbContextOptions();
            var heroesDbContext = new TourOfHeroesContext(contextOptions);
            heroesDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return heroesDbContext;
        }

        protected virtual DbContextOptions CreateDbContextOptions()
        {
            try
            {
                var result = new DbContextOptionsBuilder<TourOfHeroesContext>()
                                .UseSqlServer(CreateDbConnection())
                                .Options;

                return result;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }


        protected virtual DbConnection CreateDbConnection() =>
            new SqlConnection(ConnectionString);
    }
}
