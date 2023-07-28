using GeekStore.Core.Notifications;
using GeekStore.Cart.Application.Cart.Commands;
using Moq.AutoMock;
using GeekStore.Cart.Domain.Carts.Repositories;
using Moq;
using Bogus;
using ValidationResult = FluentValidation.Results.ValidationResult;
using GeekStore.Cart.Tests.Common;
using GeekStore.Cart.Tests.Fixtures;
using GeekStore.Cart.Domain.Carts;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class CartCommandHandlerTests : IClassFixture<CartItemFixture>
    {
        private readonly Faker _faker;
        private readonly CartItemFixture _cartItemFixture;
        public CartCommandHandlerTests(CartItemFixture cartItemFixture)
        {
            _faker = new(Constants.LOCALE);
            _cartItemFixture = cartItemFixture;
        }

        #region AddProduct
        [Fact(DisplayName = "Envio do comando válido de adição de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductCartCommand))]
        public async Task AddProductCartCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductCartCommand(userId, productId, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Decimal(min: 1));

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
        [Trait("CommandHandler", nameof(AddProductCartCommand))]
        public async Task AddProductCartCommand_CartNotExists_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductCartCommand(userId, productId, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Decimal(min: 1));

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
        [Trait("CommandHandler", nameof(AddProductCartCommand))]
        public async Task AddProductCartCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductCartCommand(Guid.Empty, Guid.Empty, string.Empty, -5, -5);

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

        [Fact(DisplayName = "Envio do comando inválido de adição de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(AddProductCartCommand))]
        public async Task AddProductCartCommand_EntityInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new AddProductCartCommand(userId, productId, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Decimal(min: 1));

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            var cart = Domain.Carts.Cart.Factory.CreateWith(Guid.Empty, null, new List<CartItem> { item });

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(cart);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }
        #endregion

        #region RemoveProduct
        [Fact(DisplayName = "Envio do comando válido de remoção de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCartCommand))]
        public async Task RemoveProductCartCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new RemoveProductCartCommand(userId, productId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(Domain.Carts.Cart.Factory.CreateWith(userId, null, new List<CartItem> { item }));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando inválido de remoção de produto no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCartCommand))]
        public async Task RemoveProductCartCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var command = new RemoveProductCartCommand(Guid.Empty, Guid.Empty);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(Guid.Empty))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de remoção de produto no carrinho com carrinho inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveProductCartCommand))]
        public async Task RemoveProductCartCommand_CartNotExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new RemoveProductCartCommand(userId, productId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de remoção de produto no carrinho ao CommandHandler com entidade inválida")]
        [Trait("CommandHandler", nameof(RemoveProductCartCommand))]
        public async Task RemoveProductCartCommand_EntityInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var command = new RemoveProductCartCommand(userId, productId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            var cart = Domain.Carts.Cart.Factory.CreateWith(Guid.Empty, null, new List<CartItem> { item });

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(cart);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }
        #endregion

        #region ApplyCoupon
        [Fact(DisplayName = "Envio do comando válido de aplicar cupom no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(ApplyCouponCartCommand))]
        public async Task ApplyCouponCartCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var couponId = Guid.NewGuid();

            var command = new ApplyCouponCartCommand(userId, couponId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(Domain.Carts.Cart.Factory.CreateWith(userId, null, new List<CartItem> { item }));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando inválido de aplicar cupom no carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(ApplyCouponCartCommand))]
        public async Task ApplyCouponCartCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.Empty;
            var couponId = Guid.Empty;

            var command = new ApplyCouponCartCommand(userId, couponId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de aplicar cupom no carrinho com carrinho inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(ApplyCouponCartCommand))]
        public async Task ApplyCouponCartCommand_CartNotExist_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var couponId = Guid.NewGuid();

            var command = new ApplyCouponCartCommand(userId, couponId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de aplicar cupom no carrinho ao CommandHandler com entidade inválida")]
        [Trait("CommandHandler", nameof(ApplyCouponCartCommand))]
        public async Task ApplyCouponCartCommand_EntityInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            var couponId = Guid.NewGuid();

            var command = new ApplyCouponCartCommand(userId, couponId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            var cart = Domain.Carts.Cart.Factory.CreateWith(Guid.Empty, null, new List<CartItem> { item });

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(cart);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }
        #endregion

        #region RemoveCoupon
        [Fact(DisplayName = "Envio do comando válido de remover cupom do carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveCouponCartCommand))]
        public async Task RemoveCouponCartCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new RemoveCouponCartCommand(userId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(Domain.Carts.Cart.Factory.CreateWith(userId, null, new List<CartItem> { item }));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando inválido de remover cupom do carrinho ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveCouponCartCommand))]
        public async Task RemoveCouponCartCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.Empty;

            var command = new RemoveCouponCartCommand(userId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de remover cupom do carrinho com carrinho inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(RemoveCouponCartCommand))]
        public async Task RemoveCouponCartCommand_CartNotExist_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new RemoveCouponCartCommand(userId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(default(Domain.Carts.Cart));

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICartRepository>()
                  .Verify(repo => repo.SetAsync(It.IsAny<Domain.Carts.Cart>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando válido de remover cupom do carrinho ao CommandHandler com entidade inválida")]
        [Trait("CommandHandler", nameof(RemoveCouponCartCommand))]
        public async Task RemoveCouponCartCommand_EntityInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new RemoveCouponCartCommand(userId);

            var commandHandler = mocker.CreateInstance<CartCommandHandler>();

            var item = _cartItemFixture.CreateValidItem();

            var cart = Domain.Carts.Cart.Factory.CreateWith(Guid.Empty, null, new List<CartItem>
            {
                item
            });

            mocker
                .GetMock<ICartRepository>()
                .Setup(repo => repo.GetCartAsync(userId))
                .ReturnsAsync(cart);

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
