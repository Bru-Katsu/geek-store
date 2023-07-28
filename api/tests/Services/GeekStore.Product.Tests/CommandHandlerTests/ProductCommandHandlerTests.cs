using FluentValidation.Results;
using GeekStore.Core.Notifications;
using GeekStore.Product.Application.Products.Commands;
using GeekStore.Product.Application.Products.Events;
using GeekStore.Product.Domain.Products.Repositories;
using GeekStore.Product.Tests.Extensions;
using GeekStore.Product.Tests.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Moq.AutoMock;

namespace GeekStore.Product.Tests.CommandHandlerTests
{
    public class ProductCommandHandlerTests : IClassFixture<ProductFixture>
    {
        private readonly ProductFixture _productFixture;

        public ProductCommandHandlerTests(ProductFixture productFixture)
        {
            _productFixture = productFixture;
        }

        #region AddProductCommand
        [Fact(DisplayName = "Envio do comando de adição de produto válido para o CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductCommand))]
        public async Task AddProduct_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new AddProductCommand(product.Name, product.Price, product.Description, product.Category, product.ImageURL);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Products.Product>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductAddedEvent>(), CancellationToken.None), Times.Once);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de adição de produto inválido para o CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductCommand))]
        public async Task AddProduct_CommandIsNotValid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new AddProductCommand(string.Empty, -5, string.Empty, string.Empty, string.Empty);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Products.Product>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductAddedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }        
        #endregion

        #region UpdateProductCommand
        [Fact(DisplayName = "Envio do comando de atualização de produto válido para o CommandHandler")]
        [Trait("CommandHandler", nameof(UpdateProductCommand))]
        public async Task UpdateProduct_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new UpdateProductCommand(product.Id, product.Name, product.Price, product.Description, product.Category, product.ImageURL);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Products.Product>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductUpdatedEvent>(), CancellationToken.None), Times.Once);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de atualização de produto válido para o CommandHandler")]
        [Trait("CommandHandler", nameof(UpdateProductCommand))]
        public async Task UpdateProduct_ProductNotExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new UpdateProductCommand(product.Id, product.Name, product.Price, product.Description, product.Category, product.ImageURL);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(default(Domain.Products.Product));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Products.Product>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductUpdatedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de atualização de produto inválido para o CommandHandler")]
        [Trait("CommandHandler", nameof(UpdateProductCommand))]
        public async Task UpdateProduct_CommandIsNotValid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new UpdateProductCommand(Guid.Empty, string.Empty, -5, string.Empty, string.Empty, string.Empty);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Products.Product>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductUpdatedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }
        #endregion

        #region RemoveProductCommand
        [Fact(DisplayName = "Envio do comando de remoção de produto válido para o CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCommand))]
        public async Task DeleteProduct_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new RemoveProductCommand(product.Id);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Delete(It.IsAny<Domain.Products.Product>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductRemovedEvent>(), CancellationToken.None), Times.Once);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de remoção de produto válido para o CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCommand))]
        public async Task DeleteProduct_ProductNotExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new RemoveProductCommand(product.Id);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(default(Domain.Products.Product));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Delete(It.IsAny<Domain.Products.Product>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductRemovedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de remoção de produto inválido para o CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCommand))]
        public async Task DeleteProduct_CommandIsNotValid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var product = _productFixture.GenerateValidProduct();
            var command = new RemoveProductCommand(Guid.Empty);

            mocker.UseDbContext<DbContext>();
            var commandHandler = mocker.CreateInstance<ProductCommandHandler>();

            mocker
                .GetMock<IProductRepository>()
                .Setup(repo => repo.GetById<Domain.Products.Product>(product.Id))
                .ReturnsAsync(product);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IProductRepository>()
                  .Verify(repo => repo.Delete(It.IsAny<Domain.Products.Product>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<ProductRemovedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }
        #endregion
    }
}
