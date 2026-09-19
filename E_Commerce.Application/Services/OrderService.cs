using E_Commerce.Application.Contracts.Dtos.Orders;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper) : IOrderService
    {
        public async Task<Result<OrderResponse>> CreateAsync(OrderToCreateDto dto, string email, CancellationToken token = default)
        {
            var basket = await basketRepository.GetBasketAsync(dto.BasketId);
            if (basket is null) return Result<OrderResponse>.Fail(Error.NotFound($"Basket with Id: {dto.BasketId} Not Found"));
            if (!basket.Items.Any()) return Result<OrderResponse>.Fail(Error.Failure($"Basket is Empty"));

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod>().GetByIdAsync(dto.DeliveryMethodId, token);
            if (deliveryMethod is null) return Result<OrderResponse>.Fail(Error.NotFound($"Delivery Method with Id: {dto.DeliveryMethodId} Not Found"));

            var productIds = basket.Items.Select(x => x.Id).ToList();
            var products = (await unitOfWork.GetRepository<Product>().GetAllAsync(new ProductsWithIdsSpec(productIds), token)).ToDictionary(p => p.Id);

            List<OrderItem> orderItems = new();

            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product)) return Result<OrderResponse>.Fail(Error.NotFound($"Product with Id: {item.Id} Not Found"));
                orderItems.Add(new OrderItem
                {
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    ProductId = item.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity
                });
            }
            var OrderAddress = mapper.Map<OrderAddress>(dto.OrderAddress);
            var subtotal = orderItems.Sum(x => x.Price * x.Quantity);
            var order = new Order
            {
                DeliveryMethod = deliveryMethod,
                Items = orderItems,
                UserEmail = email, // todo:check if useremail is valid and exists in db
                OrderAddress = OrderAddress,
                SubTotal = subtotal
                // todo: paymentintentId and paymentstatus will be set after payment is completed
            };

            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync();

            if (result <= 0) return Result<OrderResponse>.Fail(Error.Failure("Failed To Create Order"));

            await basketRepository.DeleteAsync(basket.Id, token);
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
