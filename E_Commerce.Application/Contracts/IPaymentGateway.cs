using E_Commerce.Application.Contracts.Dtos;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentGateway
    {
        Task<PaymentIntentInfo> CreatePaymentIntent(decimal amount, CancellationToken token = default);
        Task<PaymentIntentInfo> UpdatePaymentIntent(string PaymentIntentId, decimal amount, CancellationToken token = default);
    }
}
