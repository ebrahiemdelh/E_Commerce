using E_Commerce.Domain.Entities.Products;
using System.Linq.Expressions;

namespace E_Commerce.Application.Specifications
{
    public class ProductsWithIdsSpec : Specifications<Product>
    {
        public ProductsWithIdsSpec(IEnumerable<int> Ids) : base(p=>Ids.Contains(p.Id))
        {
            AsNoTracking();
        }
    }
}
