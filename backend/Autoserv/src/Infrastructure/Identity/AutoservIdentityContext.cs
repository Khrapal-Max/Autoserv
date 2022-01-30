using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AutoservIdentityContext : DbContext
    {
        public AutoservIdentityContext(DbContextOptions<AutoservIdentityContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up entities using extension method
            //modelBuilder.Configure();

            // Seeding data using extension method
            // NOTE: this method will be called every time after adding a new migration, cuz we use Bogus for seed data
            //modelBuilder.Seed();
        }
    }
}
