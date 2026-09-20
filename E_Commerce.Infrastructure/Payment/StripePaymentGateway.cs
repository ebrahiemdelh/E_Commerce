using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos;
using Stripe;
namespace E_Commerce.Infrastructure.Payment
{
    internal class StripePaymentGateway : IPaymentGateway
    {
        private readonly PaymentIntentService paymentIntentService = new();
        public StripePaymentGateway()
        {
            StripeConfiguration.ApiKey = "";
        }

        public async Task<PaymentIntentInfo> CreatePaymentIntent(decimal amount, CancellationToken token = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)Math.Round(amount * 100),
                Currency = "USD",
                AllowedPaymentMethodTypes = ["card"],
            };
            var intent = await paymentIntentService.CreateAsync(options);
            return new PaymentIntentInfo(intent.Id, intent.ClientSecret);
        }

        public async Task<PaymentIntentInfo> UpdatePaymentIntent(string PaymentIntentId, decimal amount, CancellationToken token = default)
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)Math.Round(amount * 100),
            };
            var intent = await paymentIntentService.UpdateAsync(PaymentIntentId, options);
            return new PaymentIntentInfo(intent.Id, intent.ClientSecret);
        }
    }
}
