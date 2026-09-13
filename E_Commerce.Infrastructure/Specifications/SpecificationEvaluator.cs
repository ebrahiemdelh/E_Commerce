namespace E_Commerce.Infrastructure.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> ApplySpecifications<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var query = inputQuery;
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);


            if (specification.IncludeExpressions.Any())
                query = specification.IncludeExpressions.Aggregate(query, (acc, expression) => acc.Include(expression));

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            else if (specification.OrderByDesc is not null)
                query = query.OrderByDescending(specification.OrderByDesc);

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);

            if (!specification.TrackChanges)
                query = query.AsNoTracking();
            return query;
        }
    }
}
