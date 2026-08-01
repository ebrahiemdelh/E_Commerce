namespace E_Commerce.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
