using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class PaymentController(IPaymentService paymentService) : APIBaseController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken token)
        {
            return HandleResult(await paymentService.CreateOrUpdatePaymentIntent(basketId, token));
        }
    }
}
