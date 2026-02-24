using Book.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book.Infra.Repositories.Base
{
    public abstract class RepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity>, IAsyncDisposable, IDisposable
        where TContext : DbContext
        where TEntity : class, new()
    {
        protected readonly TContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(TContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await DbSet.FindAsync(id);

        public virtual async Task<List<TEntity>> GetAllAsync() =>
            await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.AsNoTracking().Where(predicate).ToListAsync();

        public virtual IQueryable<TEntity> GetQueryable() =>
            DbSet.AsQueryable();

        public virtual async Task AddAsync(TEntity entity) =>
            await DbSet.AddAsync(entity);

        public virtual async Task AddRangeAsync(List<TEntity> entities) =>
            await DbSet.AddRangeAsync(entities);

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRangeAsync(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveRangeAsync(List<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public int SaveChanges() => Db.SaveChanges();

        public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();

        public void ClearChanges() => Db.ChangeTracker.Clear();

        public async ValueTask DisposeAsync() => await Db.DisposeAsync();

        public void Dispose() => Db.Dispose();
    }
}
