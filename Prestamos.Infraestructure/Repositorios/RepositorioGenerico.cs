using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Interfaces;
using Prestamos.Core.Modelos;
using System.Linq.Expressions;

namespace Prestamos.Infraestructure.Repositorios
{
    public class RepositorioGenerico<TEntity, TId> : IRepositorioGenerico<TEntity, TId>
        where TEntity : class
    {
        public readonly DbContext context;
        internal IQueryable<TEntity> dbQuery;
        internal ResponseResult result;

        public RepositorioGenerico(DbContext dbContext)
        {
            context = dbContext;
            dbQuery = context.Set<TEntity>().AsQueryable<TEntity>();
            result = new ResponseResult();
        }

        public virtual async Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await GetAsync(default, default, default, orderBy);
        }

        public virtual async Task<ResponseResult> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(default, default, includes, orderBy);
        }

        public virtual async Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await GetAsync(null, filter, null, orderBy);
        }

        public virtual async Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(null, filter, includes, null);
        }

        public virtual async Task<ResponseResult> FindAsync(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(null, filter, includes, orderBy);
        }

        public virtual async Task<ResponseResult> FindAndPagingAsync(
           RequestFilter requestFilter,
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await GetAsync(requestFilter, filter, null, orderBy);
        }

        public virtual async Task<ResponseResult> FindAndPagingAsync(
           RequestFilter requestFilter,
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAsync(requestFilter, filter, includes, orderBy);
        }

        internal async Task<ResponseResult> GetAsync(
            RequestFilter? requestFilter = null,
            Expression<Func<TEntity, bool>>? filters = default,
            Expression<Func<TEntity, object>>[]? includes = default,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default)
        {
            var result = new ResponseResult();

            try
            {
                // Agrego los filtros
                if (filters != null)
                    dbQuery = dbQuery.Where(filters);

                // Agrego las incluciones de entidades
                if (includes != null)
                    foreach (Expression<Func<TEntity, object>> include in includes)
                        dbQuery = dbQuery.Include(include);

                // Agrego los filtros
                if (orderBy != null)
                    dbQuery = orderBy(dbQuery);

                // Obtengo todos los registros
                if (requestFilter != null)
                {
                    result.Datos = dbQuery
                        .AsNoTracking()
                        .AsEnumerable()
                        .Skip((requestFilter.CurrentPage - 1) * requestFilter.PageSize)
                        .Take(requestFilter.PageSize)
                        .ToArray();
                }
                else
                {
                    result.Datos = dbQuery
                        .AsNoTracking()
                        .AsEnumerable()
                        .ToArray();
                }

                requestFilter ??= new();

                // Obtengo el total de registros
                result.Paginacion = new PagingResult()
                {
                    TotalRecords = dbQuery.AsNoTracking().Count(),
                    PageSize = requestFilter.PageSize,
                    CurrentPage = requestFilter.CurrentPage
                };
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            // Retorno los datos
            return await Task.FromResult(result);
        }

        public virtual async Task<ResponseResult> GetByIdAsync(TId id)
        {
            try
            {
                if (id == null)
                    return new ResponseResult(false, "Código de entidad inválido.");

                var dbSet = context.Set<TEntity>();
                if (dbSet == null)
                    return new ResponseResult(false, "Entidad inválida.");

                result.Datos = await dbSet.FindAsync(id);
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> PostAsync(TEntity entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> PostRangeAsync(TEntity[] entities)
        {
            try
            {
                int conteo = 0;
                for (int pos = 0; pos < entities.Length; pos++)
                {
                    context.Entry(entities[pos]).State = EntityState.Added;
                    conteo += 1;

                    if (conteo == 10 || pos == (entities.Length - 1))
                        await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> PutAsync(TEntity entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> PutRangeAsync(TEntity[] entities)
        {
            try
            {
                int conteo = 0;
                for (int pos = 0; pos < entities.Length; pos++)
                {
                    context.Entry(entities[pos]).State = EntityState.Modified;
                    conteo += 1;

                    if (conteo == 10 || pos == (entities.Length - 1))
                        await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> DeleteAsync(TEntity entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }

        public virtual async Task<ResponseResult> DeleteRangeAsync(TEntity[] entities)
        {
            try
            {
                foreach (var entity in entities)
                    context.Entry(entity).State = EntityState.Deleted;

                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                result = new ResponseResult(false, $"Error. {err.Message} {(err.InnerException == null ? string.Empty : err.InnerException.Message)}".Trim());
            }

            return result;
        }
    }
}

/*
 * *************************************************************************************************
 * Ejecutar Procedimiento Almacenado y retornar un objeto ******************************************
 * *************************************************************************************************
 * 
 * FormattableString query = $"Nombre_Procedimiento @Param1 = value1, @Param2 = value2";
 * var dbResult = dbContext.Tabla.FromSqlInterpolated(query).ToArray();
 * return await Task.FromResult(Ok(dbResult));
*/