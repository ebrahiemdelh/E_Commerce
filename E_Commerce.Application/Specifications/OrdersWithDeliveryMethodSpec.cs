using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Application.Specifications
{
    public class OrdersWithDeliveryMethodSpec : Specifications<Order>
    {
        public OrdersWithDeliveryMethodSpec(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            //AddInclude(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
            AsNoTracking();
        }
    }
}
