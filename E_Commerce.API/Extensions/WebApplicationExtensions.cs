using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateAndSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            foreach (var init in scope.ServiceProvider.GetRequiredService<IEnumerable<IDbInitializer>>())
            {
                await init.MigrateAsync();
                await init.SeedAsync();
            }
            return app;
        }
    }
}
