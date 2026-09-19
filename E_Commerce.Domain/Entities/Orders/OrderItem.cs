namespace E_Commerce.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public int ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Order Order{ get; set; }
    }
}