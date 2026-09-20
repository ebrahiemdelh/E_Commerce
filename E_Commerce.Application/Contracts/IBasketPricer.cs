using E_Commerce.Application.Contracts.Dtos;

namespace E_Commerce.Application.Contracts
{
    internal interface IBasketPricer
    {
        Task<Result<PricedBasket>> PriceAsync(string basketId, int? deliveryMethodId, CancellationToken token = default);
    }
}
