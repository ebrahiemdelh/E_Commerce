namespace E_Commerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<ProductDto?>> GetProductByIdAsync(int Id, CancellationToken token = default);
        Task<Result<PaginatedResult<ProductDto>>> GetProductsAsync(ProductQueryParameters queryParameters, CancellationToken token = default);
        //Task<Result<ProductDto>> CreateProductAsync(ProductCreateDto productCreateDto, CancellationToken token = default);
        //Task<Result<ProductDto>> UpdateProductAsync(int Id, ProductUpdateDto productUpdateDto, CancellationToken token = default);
        Task<Result<IEnumerable<BrandDto>>> GetBrandsAsync(CancellationToken token = default);
        Task<Result<IEnumerable<TypeDto>>> GetTypesAsync(CancellationToken token = default);
    }
}
