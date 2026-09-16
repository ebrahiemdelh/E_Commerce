using E_Commerce.API.Controllers.Base;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Products;
using E_Commerce.API.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService product) : APIBaseController
    {
        [Cache]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllAsync([FromQuery] ProductQueryParameters queryParameters, CancellationToken token = default)
        {
            var result = await product.GetProductsAsync(queryParameters, token);
            return HandleResult<PaginatedResult<ProductDto>>(result);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(int Id, CancellationToken token = default)
        {
            var result = await product.GetProductByIdAsync(Id, token);
            return HandleResult(result)!;
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync(CancellationToken token = default)
        {
            var result = await product.GetBrandsAsync(token);
            return HandleResult(result);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync(CancellationToken token = default)
        {
            var result = await product.GetTypesAsync(token);
            return HandleResult(result);
        }
    }
}
