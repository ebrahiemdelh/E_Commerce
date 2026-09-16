namespace E_Commerce.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service1");
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service2");

            //services.AddSingleton<IConnectionMultiplexer>(sp =>
            //{
            //    var configuration = ConfigurationOptions.Parse(config.GetConnectionString("RedisConnection"), true);
            //    return ConnectionMultiplexer.Connect(configuration);
            //});
            services.AddSingleton<IConnectionMultiplexer>(cfg =>
             {
                 return ConnectionMultiplexer.Connect(config.GetConnectionString("RedisConnection")!);
             });
            return services;
        }
    }
}
