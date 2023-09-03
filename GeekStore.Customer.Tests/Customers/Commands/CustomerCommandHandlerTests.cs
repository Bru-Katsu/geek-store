using GeekStore.Core.Notifications;
using MediatR;
using GeekStore.Customer.Application.Customers.Commands;
using Moq.AutoMock;
using Bogus;
using Bogus.Extensions.Brazil;
using GeekStore.Customer.Domain.Customers.Repositories;
using Moq;
using GeekStore.Customer.Application.Customers.Events;
using GeekStore.Core.Results;
using System.Linq.Expressions;
using GeekStore.Customer.Tests.Fixtures;

namespace GeekStore.Customer.Tests.Customers.Commands
{
    public class CustomerCommandHandlerTests : IClassFixture<CustomerFixture>
    {
        private readonly Faker _faker;
        private readonly CustomerFixture _customerFixture;

        public CustomerCommandHandlerTests(CustomerFixture fixture)
        {
            _faker = new();
            _customerFixture = fixture;
        }

        #region CreateCustomerCommand
        [Fact(DisplayName = "Envio do comando de criação válido de cliente ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateCustomerCommand))]
        public async Task CreateCustomerCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();
            
            var command = new CreateCustomerCommand(userId, _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsType<SuccessResult<Guid>>(result);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Customers.Customer>()), Times.Once);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerCreatedEvent>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de criação inválido de cliente ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateCustomerCommand))]
        public async Task CreateCustomerCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new CreateCustomerCommand(Guid.Empty, _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsType<FailResult<Guid>>(result);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerCreatedEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de criação com entidade inválida de cliente ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateCustomerCommand))]
        public async Task CreateCustomerCommand_EntityIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var mockedCommand = new Mock<CreateCustomerCommand>(Guid.Empty, _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            mockedCommand
                .Setup(x => x.IsValid())
                .Returns(true);

            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            //Act
            var result = await commandHandler.Handle(mockedCommand.Object, CancellationToken.None);

            //Assert
            Assert.IsType<FailResult<Guid>>(result);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerCreatedEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de criação com spec inválido de cliente ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateCustomerCommand))]
        public async Task CreateCustomerCommand_SpecificationInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new CreateCustomerCommand(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            mocker
                .GetMock<ICustomerRepository>()
                .Setup(repo => repo.Filter(It.IsAny<Expression<Func<Domain.Customers.Customer, bool>>>()))
                .ReturnsAsync(_customerFixture.CreateValidCustomers(1));

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsType<FailResult<Guid>>(result);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerCreatedEvent>(), CancellationToken.None), Times.Never);
        }
        #endregion

        #region ChangeProfileImageCommand
        [Fact(DisplayName = "Envio do comando de alteração de imagem de cliente válido ao CommandHandler")]
        [Trait("CommandHandler", nameof(ChangeProfileImageCommand))]
        public async Task ChangeProfileImageCommand_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new ChangeProfileImageCommand(userId, _faker.Image.LoremFlickrUrl());
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            mocker
                .GetMock<ICustomerRepository>()
                .Setup(repo => repo.GetById<Domain.Customers.Customer>(It.Is<Guid>(id => userId.Equals(id))))
                .ReturnsAsync(_customerFixture.CreateValidCustomer);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Customers.Customer>()), Times.Once);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerProfileImageChangedEvent>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de alteração de imagem de cliente inválido ao CommandHandler")]
        [Trait("CommandHandler", nameof(ChangeProfileImageCommand))]
        public async Task ChangeProfileImageCommand_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var command = new ChangeProfileImageCommand(Guid.Empty, _faker.Image.LoremFlickrUrl());
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerProfileImageChangedEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de alteração de imagem de cliente inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(ChangeProfileImageCommand))]
        public async Task ChangeProfileImageCommand_CustomerNotExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new ChangeProfileImageCommand(userId, _faker.Image.LoremFlickrUrl());
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerProfileImageChangedEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de alteração de imagem de cliente com entidade inválida ao CommandHandler")]
        [Trait("CommandHandler", nameof(ChangeProfileImageCommand))]
        public async Task ChangeProfileImageCommand_EntityInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var userId = Guid.NewGuid();

            var command = new ChangeProfileImageCommand(userId, _faker.Image.LoremFlickrUrl());
            var commandHandler = mocker.CreateInstance<CustomerCommandHandler>();

            mocker
                .GetMock<ICustomerRepository>()
                .Setup(repo => repo.GetById<Domain.Customers.Customer>(It.Is<Guid>(id => userId.Equals(id))))
                .ReturnsAsync(_customerFixture.CreateInvalidCustomer);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.Update(It.IsAny<Domain.Customers.Customer>()), Times.Never);

            mocker.GetMock<ICustomerRepository>()
                  .Verify(repo => repo.SaveChanges(), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<CustomerProfileImageChangedEvent>(), CancellationToken.None), Times.Never);
        }

        #endregion
    }
}
