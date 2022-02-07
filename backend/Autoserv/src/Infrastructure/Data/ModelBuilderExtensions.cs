using ApplicationCore.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        private static readonly List<string> _brandsNames = new()
        {
            "Bosh",
            "HEPU",
            "MANN",
        };

        public static void Configure(this ModelBuilder modelBuilder)
        {
            #region Unique properties

            modelBuilder.Entity<Brand>()
                .HasIndex(x => x.Name).IsUnique();
            #endregion

            #region One to one


            #endregion
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var brands = GenerateRandomBrands();

            modelBuilder.Entity<Brand>().HasData(brands);
        }

        /// <summary>
        /// Create brands
        /// </summary>
        /// <returns>ICollection<Brand></returns>
        private static ICollection<Brand> GenerateRandomBrands()
        {
            int brandId = 1;
            int index = 0;

            var testBrandsFake = new Faker<Brand>()
                .RuleFor(x => x.Id, f => brandId++)
                .RuleFor(x => x.Name, f => _brandsNames.ElementAt(index++));

            var generatedBrands = testBrandsFake.Generate(_brandsNames.Count);

            return generatedBrands;
        }
    }
}
