namespace E_Commerce.Application.Contracts.Dtos.Orders
{
    public class OrderToCreateDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public OrderAddressDto OrderAddress { get; set; } = default!;
    }
}
