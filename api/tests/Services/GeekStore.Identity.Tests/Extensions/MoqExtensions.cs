using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;

namespace GeekStore.Identity.Tests.Extensions
{
    public static class MoqExtensions
    {
        public static void UseDbContext<TContext>(this AutoMocker mocker) where TContext : DbContext
        {
            var mockRepository = new MockRepository(MockBehavior.Default);

            var dbContextMock = mockRepository.Create<DbContext>(new DbContextOptions<TContext>());
            var databaseMock = mockRepository.Create<DatabaseFacade>(dbContextMock.Object);
            var transactionMock = mockRepository.Create<IDbContextTransaction>();

            databaseMock.Setup(db => db.CurrentTransaction)
                        .Returns(transactionMock.Object);

            dbContextMock.Setup(dbContext => dbContext.Database)
                         .Returns(databaseMock.Object);

            dbContextMock.Setup(dbContext => dbContext.Database.BeginTransaction())
                         .Returns(transactionMock.Object);

            transactionMock.Setup(transaction => transaction.CommitAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

            mocker.Use(dbContextMock);
            mocker.Use(transactionMock);
        }
    }
}
