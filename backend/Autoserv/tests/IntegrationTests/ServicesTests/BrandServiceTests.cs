using ApplicationCore.Entities;
using ApplicationCore.ModelsDTO.Brand;
using ApplicationCore.Services;
using AutoMapper;
using FluentAssertions;
using IntegrationTests.FakeRepositories;
using System.Linq;
using Xunit;

namespace IntegrationTests.ServicesTests
{
    public class BrandServiceTests
    {
        private readonly FakeBrandRepository _brandRepo;

        private readonly BrandService _brandService;

        private readonly IMapper _mapper;

        private readonly MapperConfiguration _mapperConfiguration = new(cfg =>
        {
            cfg.CreateMap<BrandDTO, Brand>();

            cfg.CreateMap<Brand, BrandDTO>();

            cfg.CreateMap<NewBrandDTO, Brand>();
        });

        public BrandServiceTests()
        {
            _brandRepo = new();
            _mapper = _mapperConfiguration.CreateMapper();
            _brandService = new BrandService(_brandRepo, _mapper);
        }

        [Fact]
        public void Create_Than_GetAllBrands()
        {
            var newBrand = new NewBrandDTO()
            {
                Name = "Brand5"
            };

            var createdBrand = _brandService.CreateAsync(newBrand).GetAwaiter();
            createdBrand.Should().NotBe(newBrand);
            createdBrand.GetResult().Name.Equals(newBrand.Name);

            var brands = _brandService.GetAllAsync();
            brands.Result.Count().Equals(_brandRepo.GetAllAsync());
        }

        [Fact]
        public void Delete_Than_CetById()
        {
            int id = 1;
            _brandService.DeleteAsync(id).GetAwaiter();
            var brand = _brandService.GetByIdAsync(id);

            brand.Result.Should().BeNull();
        }

        [Fact]
        public void Update_Than_CetById()
        {
            var updatedBrand = new BrandDTO()
            {
                Id = 3,
                Name = "Brand33"
            };

            var brand = _brandService.UpdateAsync(updatedBrand);
            var actualBrand = _brandService.GetByIdAsync(updatedBrand.Id);

            actualBrand.Equals(brand);
        }
    }
}
