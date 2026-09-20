using E_Commerce.Application.Contracts.Dtos.Baskets;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class PaymentService(
        IBasketRepository basketRepository,
        IMapper mapper,
        IBasketPricer basketPricer,
        IPaymentGateway paymentGateway)
        : IPaymentService
    {
        public async Task<Result<BasketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken token = default)
        {
            var priceResult = await basketPricer.PriceAsync(basketId, null, token);

            if (priceResult.IsFailure) return Result<BasketDto>.Fail(priceResult.Errors.ToList());
            var pricedbasket = priceResult.Value;

            var basket = pricedbasket.Basket;
            basket.DeliveryCost = pricedbasket.DeliveryMethod.Price;

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var paymentInfo = await paymentGateway.CreatePaymentIntent(pricedbasket.Total, token);
                basket.PaymentIntentId = paymentInfo.PaymentIntentId;
                basket.ClientSecret = paymentInfo.ClientSecret;
            }
            else
            {
                var paymentInfo = await paymentGateway.UpdatePaymentIntent(basket.PaymentIntentId, pricedbasket.Total, token);
            }
            await basketRepository.CreateOrUpdateAsync(basket);
            return Result<BasketDto>.Ok(mapper.Map<BasketDto>(basket));
        }
    }
}
