using MediatR;

namespace GeekStore.Identity.Application.Tokens.Events
{
    internal class RefreshTokenEventHandler : INotificationHandler<RefreshTokenCreatedEvent>
    {
        public Task Handle(RefreshTokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
