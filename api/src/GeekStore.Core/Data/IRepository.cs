using GeekStore.Core.Models;
using System.Linq.Expressions;

namespace GeekStore.Core.Data
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : Entity<TEntity>;
        Task<IEnumerable<TEntity>> Filter<TEntity>() where TEntity : Entity<TEntity>;
        Task<IEnumerable<TEntity>> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TEntity>;

        void Insert<TEntity>(TEntity entity) where TEntity : Entity<TEntity>;
        void Update<TEntity>(TEntity entity) where TEntity : Entity<TEntity>;
        void Delete<TEntity>(TEntity entity) where TEntity : Entity<TEntity>;
        void Delete<TEntity>(Guid id) where TEntity : Entity<TEntity>;
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : Entity<TEntity>;
        void SaveChanges();
    }
}
