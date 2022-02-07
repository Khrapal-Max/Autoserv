using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        private readonly AutoservContext _context;

        public BrandRepository(AutoservContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateAsync(Brand item)
        {
            _context.CatalogBrands.Add(item);
            await _context.SaveChangesAsync();

            var createdBrand = await _context.CatalogBrands
                .FirstAsync(brand => brand.Id == item.Id);

            return createdBrand;
        }

        public async Task DeleteAsync(int id)
        {
            var deletedBrand = await _context.CatalogBrands
                .FirstAsync(brand => brand.Id == id);

            _context.CatalogBrands.Remove(deletedBrand);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.CatalogBrands
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.CatalogBrands
                .FirstOrDefaultAsync(brand => brand.Id == id);
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            var existBrand = await _context.CatalogBrands
                .FirstAsync(brand => brand.Id == brand.Id);

            _context.Entry(existBrand).State = EntityState.Detached;

            _context.CatalogBrands.Update(brand);
            await _context.SaveChangesAsync();

            return brand;
        }
    }
}
