using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Authorize]
    public class OrdersController(IOrderService orderService) : APIBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderToCreateDto dto, CancellationToken token = default)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var orderResult = await orderService.CreateAsync(dto, email!, token);
            return HandleResult(orderResult);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllOrders()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var ordersResult = await orderService.GetAllAsync(email!);
            return HandleResult(ordersResult);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderResponse>> GetById(Guid id)
        {
            var orderResult = await orderService.GetByIdAsync(id);
            return HandleResult(orderResult);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethodsResult = await orderService.GetDeliveryMethods();
            return HandleResult(deliveryMethodsResult);
        }
    }
}
