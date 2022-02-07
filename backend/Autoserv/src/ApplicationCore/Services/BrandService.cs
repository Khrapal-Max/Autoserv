using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ModelsDTO.Brand;
using AutoMapper;

namespace ApplicationCore.Services
{
    public class BrandService : IBaseService<NewBrandDTO, BrandDTO>
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDTO> CreateAsync(NewBrandDTO newBrandDTO)
        {
            var brandEntity = _mapper.Map<Brand>(newBrandDTO);

            var brand = await _brandRepository.CreateAsync(brandEntity);

            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task DeleteAsync(int id)
        {
            await _brandRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();

            return _mapper.Map<ICollection<BrandDTO>>(brands);
        }

        public async Task<BrandDTO> GetByIdAsync(int id)
        {
            var existBrand = await _brandRepository.GetByIdAsync(id);

            return _mapper.Map<BrandDTO>(existBrand);
        }

        public async Task<BrandDTO> UpdateAsync(BrandDTO brandDTO)
        {
            var brand = _mapper.Map<Brand>(brandDTO);

            var existBrand = await _brandRepository.UpdateAsync(brand);

            return _mapper.Map<BrandDTO>(existBrand);
        }
    }
}
