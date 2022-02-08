using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AutoservIdentityContext : IdentityDbContext
    {
        public AutoservIdentityContext(DbContextOptions<AutoservIdentityContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up entities using extension method
            //modelBuilder.Configure();

            // Seeding data using extension method
            //modelBuilder.Seed();
        }
    }
}
