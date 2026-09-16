using E_Commerce.Domain.Entities.Products;
using System.Linq.Expressions;

namespace E_Commerce.Domain.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetAllAsync(List<Expression<Func<Product, object>>> includedExpressions, CancellationToken token = default);
    }
}
