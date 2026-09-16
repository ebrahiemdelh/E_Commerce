using E_Commerce.Domain.Entities.Baskets;
using System.Text.Json;

namespace E_Commerce.Infrastructure.Repositories
{
    public class BasketRepository(IConnectionMultiplexer mux) : IBasketRepository
    {
        private readonly IDatabase _db = mux.GetDatabase();
        public async Task<Basket?> GetBasketAsync(string id, CancellationToken token = default)
        {
            var basket = await _db.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket!);
        }

        public async Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan? ttl = null, CancellationToken token = default)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreated = await _db.StringSetAsync(basket.Id, jsonBasket, ttl ?? TimeSpan.FromDays(1));
            return isCreated ? basket : null;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken token = default) => await _db.KeyDeleteAsync(id);
    }
}
