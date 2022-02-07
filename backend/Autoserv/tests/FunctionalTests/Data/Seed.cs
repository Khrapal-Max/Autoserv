using ApplicationCore.Entities;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalTests.Data
{
    public class Seed
    {
        private readonly List<string> _brandsNames = new()
        {
            "MANN",
            "TOYOTA",
            "AUDI",
        };

        public List<Brand> Brands { get; private set; }

        public Seed()
        {
            Brands = GeneratedBrands();
        }

        private List<Brand> GeneratedBrands()
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
