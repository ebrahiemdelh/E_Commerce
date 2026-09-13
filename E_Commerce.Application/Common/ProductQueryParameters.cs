namespace E_Commerce.Application.Common
{
    public class ProductQueryParameters
    {
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageIndex = 1;
        private int _pageSize = DefaultPageSize;
        public string? Search { get; set; } = null!;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions Sort { get; set; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? DefaultPageSize : value;
        }
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }
    }
    public enum ProductSortingOptions
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4
    }
}
