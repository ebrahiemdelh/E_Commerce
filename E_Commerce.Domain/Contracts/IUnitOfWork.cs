namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : BaseEntity<int>;
    }
}
