using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string id, CancellationToken token = default);
        Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan? timespan = null, CancellationToken token = default);
        Task<bool> DeleteAsync(string id, CancellationToken token = default);
    }
}
