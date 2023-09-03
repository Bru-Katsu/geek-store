using Bogus;
using Bogus.Extensions.Brazil;
using FluentValidation.Results;
using GeekStore.Core.Messages;
using GeekStore.Core.Messages.Integration;
using GeekStore.Core.Notifications;
using GeekStore.Identity.Application.Users.Events;
using GeekStore.Identity.Tests.Fixtures;
using GeekStore.MessageBus;
using Moq;
using Moq.AutoMock;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GeekStore.Identity.Tests.Users.Events
{
    public class UserEventHandlerTests : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;
        private readonly Faker _faker;
        public UserEventHandlerTests(UserFixture userFixture)
        {
            _userFixture = userFixture;
            _faker = new Faker();
        }

        [Fact(DisplayName = "Envio do evento de usuário criado ao EventHandler")]
        [Trait("EventHandler", nameof(UserCreatedEvent))]
        public async Task UserCreatedEvent_OnSuccessDispatchCreatedUserIntegrationEvent()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();

            var @event = new UserCreatedEvent(_faker.Person.FirstName, _faker.Person.LastName, _faker.Person.Cpf(), _faker.Person.DateOfBirth, user);

            var eventHandler = mocker.CreateInstance<UserEventHandler>();

            mocker.GetMock<IMessageBus>()
                  .Setup(bus => bus.RequestAsync<UserCreatedIntegrationEvent, ResponseMessage>(It.IsAny<UserCreatedIntegrationEvent>()))
                  .ReturnsAsync(new ResponseMessage(new ValidationResult()));

            //Act
            await eventHandler.Handle(@event, CancellationToken.None);

            //Assert
            mocker.GetMock<INotificationService>()
                  .Verify(notificationService => notificationService.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);
        }


        [Fact(DisplayName = "Envio do evento de usuário criado ao EventHandler, com erro após integração")]
        [Trait("EventHandler", nameof(UserCreatedEvent))]
        public async Task UserCreatedEvent_OnFailDispatchCreatedUserIntegrationEvent()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();

            var @event = new UserCreatedEvent(_faker.Person.FirstName, _faker.Person.LastName, _faker.Person.Cpf(), _faker.Person.DateOfBirth, user);

            var eventHandler = mocker.CreateInstance<UserEventHandler>();

            mocker.GetMock<IMessageBus>()
                  .Setup(bus => bus.RequestAsync<UserCreatedIntegrationEvent, ResponseMessage>(It.IsAny<UserCreatedIntegrationEvent>()))
                  .ReturnsAsync(new ResponseMessage(new ValidationResult(new []
                  {
                      new ValidationFailure("Erro", "Erro durante integração")
                  })));

            //Act
            await eventHandler.Handle(@event, CancellationToken.None);

            //Assert
            mocker.GetMock<INotificationService>()
                  .Verify(notificationService => notificationService.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Once);
        }
    }
}
