namespace E_Commerce.Application.Contracts.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> Items { get; set; } = [];

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal? DeliveryCost{ get; set; }
    }
}
