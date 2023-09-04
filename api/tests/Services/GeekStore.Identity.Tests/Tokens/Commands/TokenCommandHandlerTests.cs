using GeekStore.Core.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using GeekStore.Identity.Application.Tokens.Commands;
using GeekStore.Identity.Tests.Fixtures;
using GeekStore.Identity.Application.Tokens.Events;
using Moq;
using FluentValidation.Results;
using GeekStore.Identity.Domain.Token.Repositories;
using GeekStore.Identity.Tests.Extensions;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace GeekStore.Identity.Tests.Tokens.Commands
{
    public class TokenCommandHandlerTests : IClassFixture<UserFixture>, IClassFixture<RefreshTokenFixture>
    {
        private UserFixture _userFixture;
        private RefreshTokenFixture _refreshTokenFixture;
        public TokenCommandHandlerTests(UserFixture userFixture, RefreshTokenFixture refreshTokenFixture)
        {
            _userFixture = userFixture;
            _refreshTokenFixture = refreshTokenFixture;
        }

        #region CreateRefreshToken
        [Fact(DisplayName = "Envio do comando de criação válido de refresh token ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateRefreshTokenCommand))]
        public async Task CreateRefreshToken_CommandIsValid_CommandShouldExecuteWithSuccess()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var token = _refreshTokenFixture.CreateValidToken(user.Email);

            var command = new CreateRefreshTokenCommand(user.Email);

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["AppTokenSettings:RefreshTokenExpiration"] = "1"
            }).Build();

            mocker.Use<IConfiguration>(configuration);
            mocker.UseDbContext<DbContext>();

            var commandHandler = mocker.CreateInstance<TokenCommandHandler>();

            mocker
                .GetMock<IRefreshTokenRepository>()
                .Setup(repo => repo.GetById(user.Id))
                .ReturnsAsync(token);

            mocker
                .GetMock<IRefreshTokenRepository>()
                .Setup(repo => repo.Filter(It.IsAny<Expression<Func<Domain.Token.RefreshToken, bool>>>()))
                .ReturnsAsync(new List<Domain.Token.RefreshToken>()
                {
                    token
                });

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IRefreshTokenRepository>()
                  .Verify(repo => repo.Delete(It.Is(token, EqualityComparer<Domain.Token.RefreshToken>.Default)), Times.AtLeastOnce);

            mocker.GetMock<IRefreshTokenRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Token.RefreshToken>()), Times.Once);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Once);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Once);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<RefreshTokenCreatedEvent>(), CancellationToken.None), Times.Once);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Envio do comando de criação inválido de refresh token ao CommandHandler")]
        [Trait("CommandHandler", nameof(CreateRefreshTokenCommand))]
        public async Task CreateRefreshToken_CommandIsInvalid_CommandShouldExecuteWithFail()
        {
            //Arrange
            var mocker = new AutoMocker();

            var user = _userFixture.CreateValidUser();
            var token = _refreshTokenFixture.CreateValidToken(user.Email);

            var command = new CreateRefreshTokenCommand("email.invalido.com");

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["AppTokenSettings:RefreshTokenExpiration"] = "1"
            }).Build();

            mocker.Use<IConfiguration>(configuration);
            mocker.UseDbContext<DbContext>();

            var commandHandler = mocker.CreateInstance<TokenCommandHandler>();

            mocker
                .GetMock<IRefreshTokenRepository>()
                .Setup(repo => repo.GetById(user.Id))
                .ReturnsAsync(token);

            mocker
                .GetMock<IRefreshTokenRepository>()
                .Setup(repo => repo.Filter(It.IsAny<Expression<Func<Domain.Token.RefreshToken, bool>>>()))
                .ReturnsAsync(new List<Domain.Token.RefreshToken>()
                {
                    token
                });

            //Act
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            mocker.GetMock<IRefreshTokenRepository>()
                  .Verify(repo => repo.Delete(It.Is(token, EqualityComparer<Domain.Token.RefreshToken>.Default)), Times.Never);

            mocker.GetMock<IRefreshTokenRepository>()
                  .Verify(repo => repo.Insert(It.IsAny<Domain.Token.RefreshToken>()), Times.Never);

            mocker.GetMock<INotificationService>()
                  .Verify(service => service.AddNotifications(It.IsAny<ValidationResult>()), Times.AtLeastOnce);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.Database.BeginTransaction(), Times.Never);

            mocker.GetMock<DbContext>()
                  .Verify(context => context.SaveChangesAsync(CancellationToken.None), Times.Never);

            mocker.GetMock<IMediator>()
                  .Verify(mediator => mediator.Publish(It.IsAny<RefreshTokenCreatedEvent>(), CancellationToken.None), Times.Never);

            mocker.GetMock<IDbContextTransaction>()
                  .Verify(transaction => transaction.CommitAsync(CancellationToken.None), Times.Never);
        }
        #endregion
    }
}
