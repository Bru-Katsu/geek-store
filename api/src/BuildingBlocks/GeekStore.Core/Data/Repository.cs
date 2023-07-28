using GeekStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GeekStore.Core.Data
{
    public abstract class Repository<TDbContext> : IRepository where TDbContext : DbContext
    {
        protected readonly TDbContext _context;

        protected Repository(TDbContext context) => _context = context;

        public async Task<TEntity> GetById<TEntity>(Guid id) where TEntity : Entity
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> Filter<TEntity>() where TEntity : Entity
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            var editedEntity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;
            _context.Set<TEntity>().Update(editedEntity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(Guid id) where TEntity : Entity
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : Entity
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
