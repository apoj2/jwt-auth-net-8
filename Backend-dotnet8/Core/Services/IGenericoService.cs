


using Backend_dotnet8.Core.Entities.Util;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Backend_dotnet8.Core.Services
{
    public interface IGenericoService<TEntity> where TEntity : BaseEntity<Guid>
    {
        #region get 
        //Task<IQueryable<TEntity>> GetAllAsync(bool withoutDefaultFilters = false,
        //     Expression<Func<TEntity, bool>> predicate = null,
        //     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //     int? take = null,
        //     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        //Task<TEntity> FirstOrDefaultAsync(
        //    Expression<Func<TEntity, bool>> predicate,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity?> GetByIdAsync(Guid Id);

        Task<bool> ExistAsync(Guid id);
        #endregion

        #region Create Entity
        Task AddAsync(TEntity entity);
        Task<bool> AddAndReturnBoolAsync(TEntity entity);
        Task<TEntity> AddAndReturnAsync(TEntity entity);

        Task<string> AddAndReturnIdAsync(TEntity entity);
        Task<bool> AddListAndReturnBoolAsync(List<TEntity> entities);
        #endregion

        #region Update Entity
        Task<bool> UpdateListAsync(List<TEntity> entities);
        Task<bool> UpdateAsync(TEntity t);
        Task<TEntity> UpdateAndReturnAsync(TEntity entity);
        #endregion

        #region Delete
        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id, bool isSoftDelete = true);
        #endregion

        Task<bool> SaveAllAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();

    }
}