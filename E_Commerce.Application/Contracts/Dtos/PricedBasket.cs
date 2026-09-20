using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Application.Contracts.Dtos
{
    internal sealed record PricedBasket(Basket Basket,
        IReadOnlyList<PricedLine> Lines,
        DeliveryMethod DeliveryMethod)
    {
        public decimal SubTotal => Lines.Sum(line => line.LineTotal);

        public decimal Total => SubTotal + DeliveryMethod.Price;
    }

    internal sealed record PricedLine(
        int ProductId,
        string ProductName,
        string PictureUrl,
        decimal UnitPrice,
        int Quantity)
    {
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
