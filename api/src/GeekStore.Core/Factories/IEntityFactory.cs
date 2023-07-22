using GeekStore.Core.Models;

namespace GeekStore.Core.Factories
{
    public interface IEntityFactory<TEntity, TModel> where TEntity : Entity
    {
        TEntity CreateEntity(TModel model);
        TModel CreateModel(TEntity entity);
    }
}
