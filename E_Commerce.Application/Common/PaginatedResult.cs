namespace E_Commerce.Application.Common
{
    public sealed class PaginatedResult<TData>(int pageSize, int pageIndex, int count, IEnumerable<TData> data)
    {
        public int PageSize { get; set; } = pageSize;
        public int PageIndex { get; set; } = pageIndex;
        public int Count { get; set; } = count;
        public IEnumerable<TData> Data { get; set; } = data;
    }
}
