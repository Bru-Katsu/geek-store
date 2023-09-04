using GeekStore.Core.Models;
using System.Linq.Expressions;

namespace GeekStore.Core.Data
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> Filter();
        Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Guid id);
        IQueryable<TEntity> AsQueryable();
        void SaveChanges();
    }
}
