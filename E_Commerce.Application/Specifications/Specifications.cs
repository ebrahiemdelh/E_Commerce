using System.Linq.Expressions;

namespace E_Commerce.Application.Specifications
{
    public abstract class Specifications<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        protected Specifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> expression) => IncludeExpressions.Add(expression);
        protected void AddOrderBy(Expression<Func<TEntity, object>> expression) => OrderBy = expression;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression) => OrderByDesc = expression;


        public bool IsPaginated { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool TrackChanges { get; private set; }
        protected void AsNoTracking() => TrackChanges = true;

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
    }
}
