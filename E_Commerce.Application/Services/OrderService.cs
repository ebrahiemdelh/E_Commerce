using E_Commerce.Application.Contracts.Dtos.Orders;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper, IBasketPricer basketPricer) : IOrderService
    {
        public async Task<Result<OrderResponse>> CreateAsync(OrderToCreateDto dto, string email, CancellationToken token = default)
        {
            var priceResult = await basketPricer.PriceAsync(dto.BasketId, dto.DeliveryMethodId, token);

            if (priceResult.IsFailure) return Result<OrderResponse>.Fail(priceResult.Errors.ToList());

            var pricedBasket = priceResult.Value;

            var OrderAddress = mapper.Map<OrderAddress>(dto.OrderAddress);
            var subtotal = pricedBasket.Lines.Sum(x => x.UnitPrice * x.Quantity);

            var orderItems = pricedBasket.Lines.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                PictureUrl = i.PictureUrl,
                Price = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();

            var order = new Order
            {
                DeliveryMethod = pricedBasket.DeliveryMethod,
                Items = orderItems,
                UserEmail = email, // todo:check if useremail is valid and exists in db
                OrderAddress = OrderAddress,
                SubTotal = subtotal
                // todo: paymentintentId and paymentstatus will be set after payment is completed
            };

            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync();

            if (result <= 0) return Result<OrderResponse>.Fail(Error.Failure("Failed To Create Order"));

            await basketRepository.DeleteAsync(dto.BasketId, token);
            return mapper.Map<OrderResponse>(order);
        }

        public async Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(string email, CancellationToken token = default)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrdersWithDeliveryMethodSpec(email), token);

            return mapper.Map<List<OrderResponse>>(orders);
        }

        public async Task<Result<OrderResponse>> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderById(id), token);
            if (order is null) return Result<OrderResponse>.Fail(Error.NotFound($"Order with id: {id} Not found"));

            return mapper.Map<OrderResponse>(order);
        }

        public async Task<Result<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods(CancellationToken token = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync(false, token);

            return mapper.Map<List<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}
