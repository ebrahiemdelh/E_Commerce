namespace E_Commerce.Domain.Entities.Products
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = [];
    }
}
