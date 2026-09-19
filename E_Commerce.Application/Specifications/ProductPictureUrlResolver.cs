using E_Commerce.Application.Contracts.Dtos.Orders;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Specifications
{
    public class ProductPictureUrlResolver(IConfiguration config) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            // Implementation for resolving picture URL
            return $"{config.GetSection("BaseUrl").Value}/Files/{source.PictureUrl}";
        }
    }
    public class OrderItemPictureUrlResolver(IConfiguration config) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            // Implementation for resolving picture URL
            return $"{config.GetSection("BaseUrl").Value}/Files/{source.PictureUrl}";
        }
    }
}