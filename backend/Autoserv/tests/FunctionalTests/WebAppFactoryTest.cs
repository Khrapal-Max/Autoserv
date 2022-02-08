using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FunctionalTests
{
    public class WebAppFactoryTest<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=Autoserv_test_DB;Trusted_Connection=True;MultipleActiveResultSets=true;";

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

                s.AddDbContext<AutoservContext>((options, context) =>
                {
                    context.UseSqlServer(_connectionString);
                });

                var sp = s.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AutoservContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            });
        }
    }
}
