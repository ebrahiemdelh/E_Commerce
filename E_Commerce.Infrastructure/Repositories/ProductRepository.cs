using E_Commerce.Domain.Entities.Products;
using System.Linq.Expressions;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class ProductRepository(StoreDbContext context) : Repository<Product, int>(context), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync(List<Expression<Func<Product, object>>> includedExpressions, CancellationToken token = default)
        {
            return await context.Products.Include(p => p.ProductType)
                                   .Include(p => p.ProductBrand)
                                   .AsNoTracking()
                                   .ToListAsync(token);
        }
    }
}
