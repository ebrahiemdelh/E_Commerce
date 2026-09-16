using E_Commerce.Domain.Entities.Products;
using System.Linq.Expressions;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithBrandAndTypeSpec : Specifications<Product>
    {
        public ProductWithBrandAndTypeSpec(ProductQueryParameters Query)
            : base(BuildProductPredicate(Query))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (Query.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    //AddOrderBy(p => p.Name);
                    break;
            }

            ApplyPagination(Query.PageSize, Query.PageIndex);
            AsNoTracking();
        }
        public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        private static Expression<Func<Product, bool>> BuildProductPredicate(ProductQueryParameters query)
        {
            return
                p => (string.IsNullOrEmpty(query.Search) || p.Name.Contains(query.Search)) &&
                (!query.BrandId.HasValue || p.BrandId == query.BrandId.Value) &&
                (!query.TypeId.HasValue || p.TypeId == query.TypeId.Value);
        }
    }
}
