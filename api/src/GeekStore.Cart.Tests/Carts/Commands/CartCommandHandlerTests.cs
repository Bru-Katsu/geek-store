using GeekStore.Core.Notifications;
using GeekStore.Cart.Application.Cart.Commands;
using Moq.AutoMock;
using GeekStore.Cart.Domain.Carts.Repositories;
using Moq;
using Bogus;
using ValidationResult = FluentValidation.Results.ValidationResult;
using GeekStore.Cart.Tests.Common;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class CartCommandHandlerTests
    {
        private readonly Faker _faker;

        public CartCommandHandlerTests()
        {
            _faker = new(Constants.LOCALE);
        }

        #region AddProduct
        [Fact(DisplayName = "Envio do comando válido de adição de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductToCartCommand))]
        public async Task AddProductToCartCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductToCartCommand(userId, productId, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Decimal(min: 1));

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(Domain.Carts.Cart.Factory.NewCart(userId));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando válido de adição de produto no carrinho com carrinho inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductToCartCommand))]
        public async Task AddProductToCartCommand_CartNotExists_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductToCartCommand(userId, productId, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Decimal(min: 1));

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando inválido de adição de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductToCartCommand))]
        public async Task AddProductToCartCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductToCartCommand(Guid.Empty, Guid.Empty, string.Empty, -5, -5);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(Domain.Carts.Cart.Factory.NewCart(userId));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }
        #endregion
    }
}
