using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using tour_of_heroes_be.Models;
using tour_of_heroes_be.Services;

namespace tour_of_heroes_be
{
    public class Startup
    {
        const string CorsKey = "Access-Control-Allow-Origin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // DI config
            services.AddScoped<IHeroesService, HeroesService>();
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IDataContextFactory, DataContextFactory>();
            services.AddScoped<TourOfHeroesContext>();

            services.AddCors(options =>
            {
                var origins = Configuration.GetSection("Origins").Get<string[]>();

                options.AddPolicy(CorsKey, builder =>
                    builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddDbContext<HeroContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsKey);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}