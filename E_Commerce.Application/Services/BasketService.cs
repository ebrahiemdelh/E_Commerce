using E_Commerce.Application.Contracts.Dtos.Baskets;
using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Application.Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null)
            {
                return Error.NotFound("Basket not found");
            }
            var mappedBasket = mapper.Map<BasketDto>(basket);
            return Result<BasketDto>.Ok(mappedBasket);
        }

        public async Task<Result<BasketDto>> CreateOrUpdateAsync(BasketDto dto)
        {
            var basket = mapper.Map<Basket>(dto);

            var result = await basketRepository.CreateOrUpdateAsync(basket);
            if (result is null)
            {
                return Error.Failure("Failed to create or update basket");
            }
            var mappedResult = mapper.Map<BasketDto>(result);
            return Result<BasketDto>.Ok(mappedResult);
        }

        public async Task<Result> DeleteAsync(string id)
        {
            var isDeleted = await basketRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return Result.Fail(Error.Failure("Failed to delete basket"));
            }
            return Result.Ok();
        }
    }
}
