namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetASync(string key, CancellationToken token = default);
        Task SetAsync(string key, string value, TimeSpan ttl);
    }
}
