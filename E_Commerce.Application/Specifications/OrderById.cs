using E_Commerce.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace E_Commerce.Application.Specifications
{
    internal class OrderById : Specifications<Order>
    {
        public OrderById(Guid id) : base(o=>o.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
