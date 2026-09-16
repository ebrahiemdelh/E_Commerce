namespace E_Commerce.Domain.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetASync(string key, CancellationToken token = default);
        Task SetAsync(string key, object value, TimeSpan ttl);
    }
}
