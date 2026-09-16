using System.Text.Json;

namespace E_Commerce.Application.Contracts
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        private static readonly JsonSerializerOptions _options= new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public async Task<string?> GetASync(string key, CancellationToken token = default) => await cacheRepository.GetASync(key, token);

        public async Task SetAsync(string key, object value, TimeSpan ttl)
        {
            var json = JsonSerializer.Serialize(value, _options);
            await cacheRepository.SetAsync(key, json, ttl);
        }
    }
}
