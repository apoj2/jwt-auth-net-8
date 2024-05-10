
using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities.Exceptions;
using Backend_dotnet8.Core.Entities.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class GenericoService<TEntity> : IGenericoService<TEntity> where TEntity : BaseEntity<Guid>
    {

        private readonly AppDbContext _conexion;
        private List<TEntity> _entidades;
        private DateTime _date;
        public GenericoService(AppDbContext conexion)
        {
            _conexion = conexion ?? throw new ArgumentNullException(nameof(conexion));

            // Verificar la conexión en el constructor, si es necesario
            if (!_conexion.Database.CanConnect())
            {
                throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
            }
            _entidades = new List<TEntity>();
            _date = DateTime.Now;
        }

        /// <summary>
        /// Prepares the query to return data with diferentes options.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="include">The include.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="take">The take.</param>
        /// <returns></returns>
        //protected IQueryable<TEntity> PrepareQuery(
        //    IQueryable<TEntity> query,
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    int? take = null
        //)
        //{
        //    if (include != null)
        //    {
        //        query = include(query);
        //    }

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    if (orderBy != null)
        //    {
        //        query = orderBy(query);
        //    }

        //    if (take.HasValue)
        //    {
        //        query = query.Take(Convert.ToInt32(take));
        //    }

        //    return query;
        //}

        #region Get
        //public virtual async Task<IQueryable<TEntity>> GetAllAsync(
        //    bool withoutDefaultFilters = true,
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    int? take = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
        //)
        //{
        //    try
        //    {
        //        IQueryable<TEntity> query = _conexion.Set<TEntity>().AsQueryable();
        //        if (withoutDefaultFilters)
        //        {
        //            query = query.Where(x => x.Estado == "A");
        //        }
        //        query = PrepareQuery(query, predicate, include, orderBy, take);
        //        return await Task.Run(() => query);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ha ocurrido un error listando las entidades", ex);
        //    }

        //}

        //public virtual async Task<TEntity> FirstOrDefaultAsync(
        //    Expression<Func<TEntity, bool>> predicate,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
        //)
        //{
        //    try
        //    {
        //        IQueryable<TEntity> query = _conexion.Set<TEntity>().AsQueryable();
        //        query = PrepareQuery(query, predicate, include, orderBy);
        //        return await query.FirstOrDefaultAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ha ocurrido un error buscando la entidad", ex);
        //    }
        //}

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _conexion.Set<TEntity>().FindAsync(id);
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            try
            {
                return await _conexion.Set<TEntity>().AnyAsync(e => e.Id.Equals(id));

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        #endregion

        #region Create
        public virtual async Task AddAsync(TEntity entity)
        {

            try
            {
                entity.CreatedAt = _date;
                entity.Estate = true;

                await _conexion.AddAsync(entity);

                await SaveAllAsync();

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        public virtual async Task<bool> AddListAndReturnBoolAsync(List<TEntity> entities)
        {

            try
            {
                foreach (TEntity entity in entities)
                {
                    entity.CreatedAt = _date;
                    entity.Estate = true;

                    await _conexion.AddAsync(entity);

                    await SaveAllAsync();
                }
                return true;

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        public virtual async Task<bool> AddAndReturnBoolAsync(TEntity entity)
        {

            try
            {
                entity.CreatedAt = _date;
                entity.Estate = true;

                await _conexion.AddAsync(entity);
                await SaveAllAsync();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public async Task<TEntity> AddAndReturnAsync(TEntity entity)
        {
            try
            {
                entity.CreatedAt = _date;
                entity.Estate = true;

                await _conexion.Set<TEntity>().AddAsync(entity);
                await SaveAllAsync();

                return entity;

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public async Task<string> AddAndReturnIdAsync(TEntity entity)
        {
            try
            {
                entity.CreatedAt = _date;
                entity.Estate = true;

                await _conexion.Set<TEntity>().AddAsync(entity);
                await SaveAllAsync();

                return entity.Id.ToString();

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        #endregion

        #region Update

        public async Task<bool> UpdateListAsync(List<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.UpdateAt = _date;
                    _conexion.Set<TEntity>().Update(entity);
                }

                return await SaveAllAsync();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                entity.UpdateAt = _date;

                _conexion.Set<TEntity>().Update(entity);

                return await SaveAllAsync();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public async Task<TEntity> UpdateAndReturnAsync(TEntity entity)
        {
            try
            {
                entity.UpdateAt = _date;

                _conexion.Set<TEntity>().Update(entity);
                await SaveAllAsync();
                return entity;

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        #endregion

        #region Remove
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                entity.DeleteAt = _date;
                entity.Estate = false;
                _conexion.Set<TEntity>().Update(entity);

                return await SaveAllAsync();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id, bool isSoftDelete = true)
        {
            try
            {
                TEntity? entity = await GetByIdAsync(id);

                if (entity != null)
                {
                    if (isSoftDelete)
                    {
                        entity.DeleteAt = _date;
                        entity.Estate = false;
                        _conexion.Set<TEntity>().Update(entity);
                    }
                    else
                    {
                        _conexion.Set<TEntity>().Remove(entity);
                    }
                    return await SaveAllAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }
        #endregion 

        #region Transactions

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _conexion.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _conexion.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _conexion.Database.RollbackTransaction();
        }
        #endregion

        #region Save Changes
        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await _conexion.SaveChangesAsync() > 0;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case -2:
                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 18456:

                        throw new RepositorioException("Error de base de datos" + ex.Message, ex);
                    case 2627:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 517:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 815:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    case 4001:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                    default:

                        throw new RepositorioException("Error de base de datos " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new RepositorioException("Ha ocurrido un error insertando la entidad " + ex.Message, ex);

            }
        }

        Task<IDbContextTransaction> IGenericoService<TEntity>.BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
