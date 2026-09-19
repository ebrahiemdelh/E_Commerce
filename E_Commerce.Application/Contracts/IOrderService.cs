using E_Commerce.Application.Contracts.Dtos.Orders;

namespace E_Commerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderResponse>> CreateAsync(OrderToCreateDto dto, string email, CancellationToken token = default);
        Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(string email,CancellationToken token = default);
        Task<Result<OrderResponse>> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<Result<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods(CancellationToken token = default);
    }
}
