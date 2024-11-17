
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using System.Linq.Expressions;

namespace MusicStore.Repositories.implementations
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext context;

        protected RepositoryBase(DbContext context)
        {
            this.context = context;
        }
        public virtual async Task<ICollection<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predecate)
        {
            return await context.Set<TEntity>()
                .Where(predecate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predecate, Expression<Func<TEntity, Tkey>> orderBy)
        {
            return await context.Set<TEntity>()
                .Where(predecate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>()
                .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task UpdateAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);
            if (item is not null)
            {
                item.Status = false;
                await UpdateAsync();
            }
        }
    }
}
