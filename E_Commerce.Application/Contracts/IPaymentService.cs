using E_Commerce.Application.Contracts.Dtos.Baskets;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Result<BasketDto>> CreateOrUpdatePaymentIntent(string basketId,CancellationToken token =default);
    }
}
