using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SAQAYA.EntityFramework.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        #region GetWithCaching
        Task<IEnumerable<TEntity>> GetWithCaching(Expression<Func<TEntity, bool>> filter = null, int ExpirationMins = 5, params Expression<Func<TEntity, object>>[] includeProperties);
        #endregion

        #region Get

        TEntity GetByID(object id);
        bool Any(Expression<Func<TEntity, bool>> Where);
        TEntity GetSingle(Expression<Func<TEntity, bool>> Where, params Expression<Func<TEntity, object>>[] IncludeProperties);
        #endregion

        #region Get All
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> Where, params Expression<Func<TEntity, object>>[] IncludeProperties);
        IQueryable<TEntity> GetAsync(Expression<Func<TEntity, bool>> Where, params Expression<Func<TEntity, object>>[] IncludeProperties);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> Where, int? skip = null, int? take = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] IncludeProperties);
        IQueryable<TEntity> GetEF(Expression<Func<TEntity, bool>> Where, params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] IncludeProperties);
        #endregion

        #region GetAsync
        Task<TEntity> GetByIDAsync(object id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> Where);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> Where, params Expression<Func<TEntity, object>>[] IncludeProperties);
        Task<TEntity> GetSingleEFAsync(Expression<Func<TEntity, bool>> Where, params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] IncludeProperties);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> Where, params Expression<Func<TEntity, object>>[] IncludeProperties);
        #endregion

        #region State
        void ModifyInsertState(TEntity e);

        void ModifyUpdateState(TEntity e);
        void Unchanged(TEntity e);

        void ModifyDeleteState(TEntity e);

        #endregion

        #region Add
        public void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        #endregion

        #region Update
        public void Update(TEntity entity);
        #endregion

        #region Remove
        #endregion
        #region Core
        IQueryable<TEntity> AsQueryable();
        public void Dispose();

        #endregion


    }
}
