using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            //services.AddKeyedScoped<IDbInitializer, DbInitializer>("Service1");
            //services.AddKeyedScoped<IDbInitializer, DbInitializer>("Service2");


            return services;
        }
    }
}
