using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IBasketService basketService) : APIBaseController
    {
        [HttpGet("{id:string}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var result = await basketService.GetBasketAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(BasketDto dto)
        {
            var result = await basketService.CreateOrUpdateAsync(dto);
            return HandleResult(result);
        }

        [HttpDelete("{id:string}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await basketService.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}
