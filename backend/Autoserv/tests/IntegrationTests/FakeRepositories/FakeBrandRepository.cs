using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests.FakeRepositories
{
    internal class FakeBrandRepository : IRepository<Brand>
    {
        private readonly ICollection<Brand> _brands = new List<Brand>()
        {
            new Brand()
            {
                Id = 1,
                Name = "Brand1"
            },
            new Brand()
            {
                Id = 2,
                Name = "Brand2"
            },
            new Brand()
            {
                Id = 3,
                Name = "Brand3"
            },
            new Brand()
            {
                Id = 4,
                Name = "Brand4"
            }
        };

        public Task<Brand> CreateAsync(Brand item)
        {
            var brand = _brands.OrderByDescending(x => x.Id).FirstOrDefault()!;
            item.Id = brand.Id + 1;
            _brands.Add(item);
            return Task.Run(() => _brands.FirstOrDefault(x => x.Id == item.Id))!;
        }

        public Task DeleteAsync(int id)
        {
            var brand = _brands.FirstOrDefault(x => x.Id == id);
            return Task.Run(() => _brands.Remove(brand!));
        }

        public Task<IEnumerable<Brand>> GetAllAsync()
        {
            return Task.Run(() => _brands.AsEnumerable());
        }

        public Task<Brand> GetByIdAsync(int id)
        {
            return Task.Run(() => _brands.FirstOrDefault(x => x.Id == id))!;
        }

        public Task<Brand> UpdateAsync(Brand item)
        {
            var brand = _brands.FirstOrDefault(x => x.Id == item.Id);
            _brands.Remove(brand!);
            _brands.Add(item);

            return Task.Run(() => _brands.FirstOrDefault(x => x.Id == item.Id))!;
        }
    }
}
