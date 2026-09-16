using E_Commerce.Application.Contracts.Dtos.Baskets;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string id);
        Task<Result<BasketDto>> CreateOrUpdateAsync(BasketDto dto);
        Task<Result> DeleteAsync(string id);
    }
}
