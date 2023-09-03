namespace GeekStore.Core.Specification
{
    public interface IAsyncSpecification<T> where T : class
    {
        Task<bool> IsSatisfiedBy(T entity);
    }
}
