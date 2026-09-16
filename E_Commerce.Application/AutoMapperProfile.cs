using E_Commerce.Application.Contracts.Dtos.Baskets;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Brand, BrandDto>();

            CreateMap<ProductType, TypeDto>();


            CreateMap<Basket, BasketDto>()
                .ReverseMap();
            CreateMap<BasketItem, BasketItemDto>()
                .ReverseMap();
        }
    }
}
