using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace E_Commerce.Infrastructure.Data
{
    internal class DbInitializer(ILogger<DbInitializer> logger, StoreDbContext dbContext) : IDbInitializer
    {
        public async Task MigrateAsync(CancellationToken token = default)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(token);
            if (!pendingMigrations.Any()) return;

            logger.LogInformation($"Applying {pendingMigrations.Count()} Migrations.");
            await dbContext.Database.MigrateAsync(token);
        }

        public async Task SeedAsync(CancellationToken token = default)
        {
            try
            {
                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedIfEmptyAsync<Product>(seedRoot, "products.json", token);
                await SeedIfEmptyAsync<Brand>(seedRoot, "brands.json", token);
                await SeedIfEmptyAsync<ProductType>(seedRoot, "types.json", token);

                var rows = await dbContext.SaveChangesAsync(token);
                logger.LogInformation($"{rows} Rows Seeded");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Data Seeding Failed");
            }
        }
        private async Task SeedIfEmptyAsync<TEntity>(string rootPath, string fileName, CancellationToken token = default) where TEntity : BaseEntity<int>
            => await SeedIfEmptyAsync<TEntity, int>(rootPath, fileName, token);

        private async Task SeedIfEmptyAsync<TEntity, TKey>(string rootPath, string fileName, CancellationToken token = default) where TEntity : BaseEntity<TKey>
        {
            if (await dbContext.Set<TEntity>().AnyAsync())
                return;
            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File {fileName} Not Found in Path {rootPath}.");
                return;
            }

            await using var fileStream = File.OpenRead(filePath);
            var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(fileStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }, token);

            if (data is { Count: > 0 })
                dbContext.Set<TEntity>().AddRange(data);

        }
    }
}
