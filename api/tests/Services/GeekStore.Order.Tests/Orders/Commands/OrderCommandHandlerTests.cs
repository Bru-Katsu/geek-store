using Bogus;
using GeekStore.Core.Notifications;
using GeekStore.Order.Application.Orders.Commands;
using GeekStore.Order.Application.Orders.DTOs;
using GeekStore.Order.Application.Orders.Events;
using GeekStore.Order.Domain.Orders.Repositories;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace GeekStore.Order.Tests.Orders.Commands
{
    public class OrderCommandHandlerTests
    {
        private readonly Faker _faker;

        public OrderCommandHandlerTests()
        {
            _faker = new();
        }

        #region CreateOrderCommand
        [Fact(DisplayName = "Envio do comando válido de criação de pedido ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateOrderCommand))]
        public async Task CreateOrderCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(userId, address, coupon, items);

            var commandHandler = mocker.CreateInstance<OrderCommandHandler>();

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IOrderRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Orders.Order>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando inválido de criação de pedido ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateOrderCommand))]
        public async Task CreateOrderCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var address = new AddressDTO(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(userId, address, coupon, items);

            var commandHandler = mocker.CreateInstance<OrderCommandHandler>();

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IOrderRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Orders.Order>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando inválido provocando uma entidade inválida ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateOrderCommand))]
        public async Task CreateOrderCommand_EntityIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var address = new AddressDTO(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(userId, address, coupon, items);
            var commandMock = new Mock<CreateOrderCommand>(userId, address, coupon, items);

            commandMock.Setup(x => x.IsValid())
                       .Returns(true);

            var commandHandler = mocker.CreateInstance<OrderCommandHandler>();

            //Act
            await commandHandler.Handle(commandMock.Object, CancellationToken.None);

            //Assert
            mocker.GetMock<IOrderRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Orders.Order>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact(DisplayName = "Envio do comando inválido provocando entidades de itens inválidos ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateOrderCommand))]
        public async Task CreateOrderCommand_ItemsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.Empty, string.Empty, string.Empty, -1, -1));

            var command = new CreateOrderCommand(userId, address, coupon, items);
            var commandMock = new Mock<CreateOrderCommand>(userId, address, coupon, items);

            commandMock.Setup(x => x.IsValid())
                       .Returns(true);

            var commandHandler = mocker.CreateInstance<OrderCommandHandler>();

            //Act
            await commandHandler.Handle(commandMock.Object, CancellationToken.None);

            //Assert
            mocker.GetMock<IOrderRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Orders.Order>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Exactly(items.Count));

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        #endregion
    }
}
