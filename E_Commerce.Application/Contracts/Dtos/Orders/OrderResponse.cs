using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Application.Contracts.Dtos.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } = default!;


        public OrderAddressDto OrderAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;

        public decimal DeliveryCost { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }
        public string PaymentStatus { get; set; } = default!;

        public string? PaymentIntentId { get; set; } = default!;

        public decimal Total { get; set; }
    }
}
