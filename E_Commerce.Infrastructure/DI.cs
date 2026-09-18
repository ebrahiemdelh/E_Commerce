using E_Commerce.Application.Contracts;
using E_Commerce.Infrastructure.Identity.Entities;
using E_Commerce.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var connection = config.GetConnectionString("IdentityConnection");
                ArgumentNullException.ThrowIfNullOrEmpty(connection);
                options.UseSqlServer(connection);
            });

            services.AddSingleton<IConnectionMultiplexer>(cfg =>
             {
                 return ConnectionMultiplexer.Connect(config.GetConnectionString("RedisConnection")!);
             });

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            //services.AddSingleton<IConnectionMultiplexer>(sp =>
            //{
            //    var configuration = ConfigurationOptions.Parse(config.GetConnectionString("RedisConnection"), true);
            //    return ConnectionMultiplexer.Connect(configuration);
            //});

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IDbInitializer, StoreIdentityDbInitializer>();
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service1");
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service2");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserStore, UserStore>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }
    }
}
