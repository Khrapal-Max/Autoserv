using ApplicationCore.Entities;
using ApplicationCore.ModelsDTO.Brand;
using AutoMapper;

namespace ApplicationCore.MappingProfiles
{
    public sealed class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandDTO, Brand>();

            CreateMap<Brand, BrandDTO>();

            CreateMap<NewBrandDTO, Brand>();
        }
    }
}
