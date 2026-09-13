namespace E_Commerce.Domain.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken token = default);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken token = default);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token = default);
        Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken token = default);
        void Add(TEntity entity);
        void Update(TKey id, TEntity entity);
        void Delete(TKey id, TEntity entity);

        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken token = default);
    }
}