using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services
{
    internal class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<Result<IEnumerable<ProductDto>>> GetProductsAsync(CancellationToken token = default)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(token: token);
        }

        public Task<Result<ProductDto?>> GetProductByIdAsync(int Id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        //public Task<Result<ProductDto>> CreateProductAsync(ProductCreateDto productCreateDto, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Result<ProductDto>> UpdateProductAsync(int Id, ProductUpdateDto productUpdateDto, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}


        public Task<Result<IEnumerable<BrandDto>>> GetBrandsAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<TypeDto>>> GetTypesAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
