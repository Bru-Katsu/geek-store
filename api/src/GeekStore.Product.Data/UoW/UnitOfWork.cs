using GeekStore.Core.UoW;
using GeekStore.Product.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace GeekStore.Product.Data.UoW
{
    internal class ProductUnitOfWork : IUnitOfWork
    {
        private readonly ProductDataContext _dataContext;
        private IDbContextTransaction _transaction;
        public ProductUnitOfWork(ProductDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dataContext.Database.BeginTransaction();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _transaction?.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _transaction?.RollbackAsync(cancellationToken);
        }
    }
}
