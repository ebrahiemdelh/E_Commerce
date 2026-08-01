namespace E_Commerce.Application.Contracts.Dtos.Products
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public string Brand { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
