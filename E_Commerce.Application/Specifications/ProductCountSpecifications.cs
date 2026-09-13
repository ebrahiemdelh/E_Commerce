using System.Linq.Expressions;

namespace E_Commerce.Application.Specifications
{
    public class ProductCountSpecifications(ProductQueryParameters query) : Specifications<Product>(BuildProductPredicate(query))
    {
        private static Expression<Func<Product, bool>>? BuildProductPredicate(ProductQueryParameters query)
        {
            return p => (!query.BrandId.HasValue || p.BrandId == query.BrandId.Value) &&
                        (!query.TypeId.HasValue || p.TypeId == query.TypeId.Value) &&
                        (string.IsNullOrWhiteSpace(query.Search) || p.Name.Contains(query.Search, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
