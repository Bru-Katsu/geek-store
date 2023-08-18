namespace GeekStore.Core.Specification
{
    public interface ISpecification<T> where T : class
    {
        bool IsSatisfiedBy(T entity);
    }
}
