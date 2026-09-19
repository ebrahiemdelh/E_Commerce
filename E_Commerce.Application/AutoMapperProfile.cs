using E_Commerce.Application.Contracts.Dtos.Baskets;
using E_Commerce.Application.Contracts.Dtos.Orders;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ProductProfile();
            OrderProfile();


        }
        public void ProductProfile()
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
        public void OrderProfile()
        {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, opt => opt.MapFrom(s => s.DeliveryMethod.Price))
                .ForMember(d => d.Total, opt => opt.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
