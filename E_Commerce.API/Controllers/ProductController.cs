using E_Commerce.Application.Contracts;
using E_Commerce.Application.Contracts.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService product) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync(CancellationToken token = default)
        {
            return Ok(await product.GetProductsAsync(token));
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(int Id, CancellationToken token = default)
        {
            return Ok(await product.GetProductByIdAsync(Id, token));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync(CancellationToken token = default)
        {
            return Ok(await product.GetBrandsAsync(token));
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync(CancellationToken token = default)
        {
            return Ok(await product.GetTypesAsync(token));
        }
    }
}
