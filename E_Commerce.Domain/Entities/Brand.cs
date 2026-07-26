namespace E_Commerce.Domain.Entities
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = [];
    }
}
