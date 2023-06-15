namespace GeekStore.Core.UoW
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
