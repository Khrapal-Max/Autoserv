using FunctionalTests.Data;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FunctionalTests
{
    public class WebAppFactoryTest<T> : WebApplicationFactory<Program> where T : Program
    {
        private readonly string _connectionString;

        public WebAppFactoryTest()
        {
            var builder = WebApplication.CreateBuilder();
            _connectionString = builder.Configuration.GetConnectionString("AutoservConnectionTestDb");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(s =>
            {
                var descriptor = s.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<AutoservContext>));

                if (descriptor != null)
                {
                    s.Remove(descriptor);
                }

                s.AddDbContext<AutoservContext>(o =>
                {
                    o.UseSqlServer(_connectionString);
                });

                var sp = s.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<AutoservContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();               

                var seed = new Seed();
                context.CatalogBrands.AddRange(seed.Brands);
            });
        }
    }
}
