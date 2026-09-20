using E_Commerce.Application.Contracts.Dtos;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class BasketPricer(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IBasketPricer
    {
        public async Task<Result<PricedBasket>> PriceAsync(string basketId, int? deliveryMethodId, CancellationToken token = default)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket is null) return Result<PricedBasket>.Fail(Error.NotFound($"Basket with Id: {basketId} Not Found"));
            if (!basket.Items.Any()) return Result<PricedBasket>.Fail(Error.Failure($"Basket is Empty"));

            var chosenDeliveryMethodId = deliveryMethodId ?? basket.DeliveryMethodId;
            if (!chosenDeliveryMethodId.HasValue) return Result<PricedBasket>.Fail(Error.Failure($"Delivery Method Id Is null"));
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod>().GetByIdAsync(deliveryMethodId.Value, token);
            if (deliveryMethod is null) return Result<PricedBasket>.Fail(Error.NotFound($"Delivery Method with Id: {deliveryMethodId.Value} Not Found"));

            var productIds = basket.Items.Select(x => x.Id).ToList();
            var products = (await unitOfWork.GetRepository<Product>().GetAllAsync(new ProductsWithIdsSpec(productIds), token)).ToDictionary(p => p.Id);

            List<PricedLine> lines = new();

            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product)) return Result<PricedBasket>.Fail(Error.NotFound($"Product with Id: {item.Id} Not Found"));
                lines.Add(new PricedLine
                (
                    item.Id,
                    product.Name,
                    product.PictureUrl,
                    product.Price,
                    item.Quantity));
            }
            return new PricedBasket(basket, lines, deliveryMethod);
        }
    }
}
