using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Contracts.Dtos.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; } = 0;
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; } = 0;
    }
}
