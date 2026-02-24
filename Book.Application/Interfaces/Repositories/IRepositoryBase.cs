using System.Linq.Expressions;

namespace Book.Application.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IAsyncDisposable, IDisposable
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetQueryable();

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(List<TEntity> entities);

        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(List<TEntity> entities);

        Task<int> SaveChangesAsync();
        int SaveChanges();
        void ClearChanges();
    }
}
