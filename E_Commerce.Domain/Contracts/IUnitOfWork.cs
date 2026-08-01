namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken token = default);
        public Task<IRepository<TEntity, TKey>> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        public Task<IRepository<TEntity, int>> GetRepository<TEntity>() where TEntity : BaseEntity<int>;
    }
}
