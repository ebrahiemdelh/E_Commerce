namespace E_Commerce.Application
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name));

            CreateMap<Brand, BrandDto>();

            CreateMap<ProductType, TypeDto>();
        }
    }
}
