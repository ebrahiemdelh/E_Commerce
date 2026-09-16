namespace E_Commerce.Domain.Entities.Baskets
{
    public class Basket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
