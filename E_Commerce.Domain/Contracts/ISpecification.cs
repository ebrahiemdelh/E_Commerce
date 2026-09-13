using System.Linq.Expressions;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, bool>> Criteria { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDesc { get; }
        public bool IsPaginated { get; }
        public int Skip { get; }
        public int Take { get; }
        public bool TrackChanges { get; }
    }
}