using E_Commerce.Application.Contracts.Dtos.Orders;

namespace E_Commerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderResponse>> CreateAsync(OrderToCreateDto dto, string email, CancellationToken token = default);
    }
}
