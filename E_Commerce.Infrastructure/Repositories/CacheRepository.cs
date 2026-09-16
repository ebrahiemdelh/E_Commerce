namespace E_Commerce.Infrastructure.Repositories
{
    public class CacheRepository(IConnectionMultiplexer mux) : ICacheRepository
    {
        private readonly IDatabase _db = mux.GetDatabase();
        public async Task<string?> GetASync(string key, CancellationToken token = default)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task SetAsync(string key, string value, TimeSpan ttl)
        {
            await _db.StringSetAsync(key, value, ttl);
        }
    }
}