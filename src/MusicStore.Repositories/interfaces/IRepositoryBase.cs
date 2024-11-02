using MusicStore.Entities;
using System.Linq.Expressions;

namespace MusicStore.Repositories.interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<ICollection<TEntity>> GetAsync();
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predecate); //el predicado Permite utilizar expresones lambda para hacer filtro
        Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predecate, Expression<Func<TEntity, Tkey>> orderBy);
        Task<TEntity?> GetAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
