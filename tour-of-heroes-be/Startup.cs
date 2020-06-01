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
            var cs = Configuration["Database:ConnectionString"];
            services.AddDbContext<TourOfHeroesContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.AddCors(options =>
            {
                var origins = Configuration.GetSection("Origins").Get<string[]>();

                options.AddPolicy(CorsKey, builder =>
                    builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers();

            // DI config
            services.AddScoped<IDataContextFactory, DataContextFactory>();
            services.AddScoped<IHeroesService, HeroesService>();
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