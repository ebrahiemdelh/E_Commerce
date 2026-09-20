namespace E_Commerce.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketPricer, BasketPricer>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddAutoMapper(cfg => { }, typeof(DI).Assembly);



            return services;
        }
    }
}
