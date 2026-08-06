namespace E_Commerce.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(DI).Assembly);



            return services;
        }
    }
}
