using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AutoservContext : DbContext
    {
        public DbSet<Brand> CatalogBrands { get; private set; } = null!;

        public AutoservContext(DbContextOptions<AutoservContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up entities using extension method
            modelBuilder.Configure();

            // Seeding data using extension method
            modelBuilder.Seed();
        }
    }
}
