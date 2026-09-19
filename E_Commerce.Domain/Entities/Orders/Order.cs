using System.Net.Mail;

namespace E_Commerce.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } = default!;


        public OrderAddress OrderAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public string? PaymentIntentId { get; set; } = default!;
        //todo: deliveryCost
    }
    public enum OrderPaymentStatus
    {
        Pending = 1,
        Failed,
        Success
    }
}
