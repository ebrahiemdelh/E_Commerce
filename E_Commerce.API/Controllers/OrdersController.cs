using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    public class OrdersController(IOrderService orderService) : APIBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderToCreateDto dto, CancellationToken token = default)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var orderResult = await orderService.CreateAsync(dto, email!, token);
            return HandleResult(orderResult);
        }
    }
}
