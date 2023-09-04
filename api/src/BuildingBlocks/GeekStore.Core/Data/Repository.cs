using GeekStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GeekStore.Core.Data
{
    public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity> where TDbContext : DbContext 
                                                                                 where TEntity : Entity
    {
        protected readonly TDbContext _context;

        protected Repository(TDbContext context) => _context = context;

        public async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> Filter()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            var editedEntity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;
            _context.Set<TEntity>().Update(editedEntity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(Guid id)
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
