namespace E_Commerce.Infrastructure.Repositories
{
    internal class Repository<TEntity, TKey>(StoreDbContext context)
        : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken token = default)
        => trackChanges ? await context.Set<TEntity>().ToListAsync(cancellationToken: token) :
            await context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken: token);


        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token = default)
        => await context.Set<TEntity>().FindAsync([id], cancellationToken: token);

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TKey id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
