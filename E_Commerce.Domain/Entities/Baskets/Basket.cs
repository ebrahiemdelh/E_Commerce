namespace E_Commerce.Domain.Entities.Baskets
{
    public class Basket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = [];

        public int? DeliveryMethodId { get; set; }
        public decimal? DeliveryCost { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
