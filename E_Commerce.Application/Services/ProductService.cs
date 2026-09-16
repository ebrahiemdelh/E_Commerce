using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Result<PaginatedResult<ProductDto>>> GetProductsAsync(ProductQueryParameters queryParameters, CancellationToken token = default)
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(new ProductWithBrandAndTypeSpec(queryParameters), token: token);

            var count = await unitOfWork.GetRepository<Product>().CountAsync(new ProductCountSpecifications(queryParameters), token: token);

            var data = mapper.Map<IEnumerable<ProductDto>>(products);
            var result = new PaginatedResult<ProductDto>(queryParameters.PageSize, queryParameters.PageIndex, count, data);
            return result;
        }

        public async Task<Result<ProductDto?>> GetProductByIdAsync(int Id, CancellationToken token = default)
        {
            var product = await unitOfWork.GetRepository<Product>().GetAsync(new ProductWithBrandAndTypeSpec(Id), token);

            if (product == null) return Error.NotFound($"Product With Id {Id} not found");

            return mapper.Map<ProductDto?>(product);
        }

        //public Task<Result<ProductDto>> CreateProductAsync(ProductCreateDto productCreateDto, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Result<ProductDto>> UpdateProductAsync(int Id, ProductUpdateDto productUpdateDto, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<Result<IEnumerable<BrandDto>>> GetBrandsAsync(CancellationToken token = default)
        {
            var brands = await unitOfWork.GetRepository<Brand>().GetAllAsync(token: token);
            var data = mapper.Map<IEnumerable<BrandDto>>(brands);
            return Result<IEnumerable<BrandDto>>.Ok(data);
        }

        public async Task<Result<IEnumerable<TypeDto>>> GetTypesAsync(CancellationToken token = default)
        {
            var types = await unitOfWork.GetRepository<ProductType>().GetAllAsync(token: token);
            var data = mapper.Map<IEnumerable<TypeDto>>(types);
            return Result<IEnumerable<TypeDto>>.Ok(data);
        }
    }
}
