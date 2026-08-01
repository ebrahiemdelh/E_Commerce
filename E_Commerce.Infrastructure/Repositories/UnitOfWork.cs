namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public async Task<int> SaveChangesAsync(CancellationToken token = default) => await context.SaveChangesAsync(token);
        public async Task<IRepository<TEntity, int>> GetRepository<TEntity>() where TEntity : BaseEntity<int> => await GetRepository<TEntity, int>();

        public async Task<IRepository<TEntity, TKey>> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value)) return (value as Repository<TEntity, TKey>)!;

            var repo = new Repository<TEntity, TKey>(context);
            _repositories.Add(typeName, repo);
            return repo;
        }
    }
}
