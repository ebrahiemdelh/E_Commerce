namespace E_Commerce.Application
{
    internal class ProductPictureUrlResolver(IConfiguration config) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            // Implementation for resolving picture URL
            return $"{config.GetSection("BaseUrl").Value}/Files/{source.PictureUrl}";
        }
    }
}