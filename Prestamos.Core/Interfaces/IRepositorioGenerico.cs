using Prestamos.Core.Modelos;
using System.Linq.Expressions;

namespace Prestamos.Core.Interfaces
{
    public interface IRepositorioGenerico<TEntity, TKey> where TEntity : class
    {
        Task<ResponseResult> GetAllAsync();
        
        Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes);

        Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           params Expression<Func<TEntity, object>>[] includes);

        Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           params Expression<Func<TEntity, object>>[] includes);

        Task<ResponseResult> FindAndPagingAsync(
           RequestFilter requestFilter,
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByl);

        Task<ResponseResult> FindAndPagingAsync(
           RequestFilter requestFilter,
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           params Expression<Func<TEntity, object>>[] includes);

        Task<ResponseResult> GetByIdAsync(TKey id);

        Task<ResponseResult> PostAsync(TEntity item);

        Task<ResponseResult> PostRangeAsync(TEntity[] items);

        Task<ResponseResult> PutAsync(TEntity item);

        Task<ResponseResult> PutRangeAsync(TEntity[] items);

        Task<ResponseResult> DeleteAsync(TEntity item);

        Task<ResponseResult> DeleteRangeAsync(TEntity[] items);
    }
}
