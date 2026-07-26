namespace E_Commerce.Domain.Contracts
{
    public interface IDbInitializer
    {
        public Task MigrateAsync(CancellationToken token = default);
        public Task SeedAsync(CancellationToken token = default);
    }
}
