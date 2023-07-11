using GeekStore.Core.Notifications;
using GeekStore.Identity.Tests.Fixtures;
using MediatR;
using Moq.AutoMock;
using Moq;
using GeekStore.Identity.Application.Users.Commands;
using GeekStore.Identity.Domain.User;
using Microsoft.AspNetCore.Identity;
using Bogus;
using GeekStore.Identity.Application.Users.Events;

namespace GeekStore.Identity.Tests.Users.Commands
{
    public class UserCommandHandlerTests : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;
        private readonly Faker _faker;
        public UserCommandHandlerTests(UserFixture userFixture)
        {
            _userFixture = userFixture;
            _faker = new Faker();
        }

        #region CreateUser
        [Fact(DisplayName = "Envio do comando de criação válido de usuário ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateUserCommand))]
        public async Task CreateUser_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();

            var command = new CreateUserCommand(user.Email, _faker.Internet.Password());

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<UserManager<User>>()
                  .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                  .ReturnsAsync(new IdentityResult());

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<UserCreatedEvent>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de criação inválido de usuário ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateUserCommand))]
        public async Task CreateUser_CommandInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var command = new CreateUserCommand("email.invalido.com", string.Empty);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<UserManager<User>>()
                  .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                  .ReturnsAsync(new IdentityResult());

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Never);

            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<UserCreatedEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de criação válido de usuário com email já existente ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateUserCommand))]
        public async Task CreateUser_UserExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();

            var command = new CreateUserCommand(user.Email, _faker.Internet.Password());

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<UserManager<User>>()
                  .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                  .ReturnsAsync(new IdentityResult());

            mocker.GetMock<UserManager<User>>()
                  .Setup(manager => manager.FindByEmailAsync(user.Email))
                  .ReturnsAsync(user);

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mocker.GetMock<UserManager<User>>()
                  .Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<UserCreatedEvent>(), CancellationToken.None), Times.Never);
        }
        #endregion

        #region Login
        [Fact(DisplayName = "Envio do comando de login válido ao CommandHandler")]
        [Trait("CommandHandler", nameof(LoginCommand))]
        public async Task Login_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var password = "senha.123";

            var command = new LoginCommand(user.Email, password);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<SignInManager<User>>()
                  .Setup(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()))
                  .ReturnsAsync(SignInResult.Success);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result);

            mocker.GetMock<SignInManager<User>>()
                  .Verify(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Envio do comando de login válido de usuário inexistente ao CommandHandler")]
        [Trait("CommandHandler", nameof(LoginCommand))]
        public async Task Login_UserNotExists_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var password = "senha.123";

            var command = new LoginCommand(user.Email, password);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<SignInManager<User>>()
                  .Setup(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()))
                  .ReturnsAsync(SignInResult.Failed);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result);

            mocker.GetMock<SignInManager<User>>()
                  .Verify(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Envio do comando de login válido com usuário em lock ao CommandHandler")]
        [Trait("CommandHandler", nameof(LoginCommand))]
        public async Task Login_UserInLockOut_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var password = "senha.123";

            var command = new LoginCommand(user.Email, password);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<SignInManager<User>>()
                  .Setup(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()))
                  .ReturnsAsync(SignInResult.LockedOut);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result);

            mocker.GetMock<SignInManager<User>>()
                  .Verify(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Envio do comando de login válido com usuário bloqueado ao CommandHandler")]
        [Trait("CommandHandler", nameof(LoginCommand))]
        public async Task Login_UserBlocked_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var password = "senha.123";

            var command = new LoginCommand(user.Email, password);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<SignInManager<User>>()
                  .Setup(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()))
                  .ReturnsAsync(SignInResult.NotAllowed);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result);

            mocker.GetMock<SignInManager<User>>()
                  .Verify(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Envio do comando de login inválido ao CommandHandler")]
        [Trait("CommandHandler", nameof(LoginCommand))]
        public async Task Login_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var password = "senha.123";

            var command = new LoginCommand("email.invalido.com", string.Empty);

            var commandHandler = mocker.CreateInstance<UserCommandHandler>();

            mocker.GetMock<SignInManager<User>>()
                  .Setup(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()))
                  .ReturnsAsync(SignInResult.Success);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result);

            mocker.GetMock<SignInManager<User>>()
                  .Verify(manager => manager.PasswordSignInAsync(user.Email, password, It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<FluentValidation.Results.ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
        #endregion
    }
}
