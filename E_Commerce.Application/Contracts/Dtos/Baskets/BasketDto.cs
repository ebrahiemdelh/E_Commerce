namespace E_Commerce.Application.Contracts.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
