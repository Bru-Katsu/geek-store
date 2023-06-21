using GeekStore.Core.Models;
using System.Linq.Expressions;

namespace GeekStore.Core.Data
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : Entity;
        Task<IEnumerable<TEntity>> Filter<TEntity>() where TEntity : Entity;
        Task<IEnumerable<TEntity>> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;

        void Insert<TEntity>(TEntity entity) where TEntity : Entity;
        void Update<TEntity>(TEntity entity) where TEntity : Entity;
        void Delete<TEntity>(TEntity entity) where TEntity : Entity;
        void Delete<TEntity>(Guid id) where TEntity : Entity;
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : Entity;
        void SaveChanges();
    }
}
