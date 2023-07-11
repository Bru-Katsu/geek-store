using MediatR;

namespace GeekStore.Identity.Application.Users.Events
{
    internal class UserEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
