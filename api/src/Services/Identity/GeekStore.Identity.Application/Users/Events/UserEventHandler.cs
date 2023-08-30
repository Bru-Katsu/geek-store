using GeekStore.Core.Messages;
using GeekStore.Core.Messages.Integration;
using GeekStore.Core.Notifications;
using GeekStore.Identity.Application.Users.Commands;
using GeekStore.MessageBus;
using MediatR;

namespace GeekStore.Identity.Application.Users.Events
{
    public class UserEventHandler : INotificationHandler<UserCreatedEvent>,
                                    INotificationHandler<UserDeletedEvent>
    {
        private readonly IMessageBus _bus;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        public UserEventHandler(IMessageBus bus,
                                INotificationService notificationService,
                                IMediator mediator)
        {
            _bus = bus;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var response = await _bus.RequestAsync<UserCreatedIntegrationEvent, ResponseMessage>(new UserCreatedIntegrationEvent(notification.AggregateId, 
                                                                                                                                 notification.Name, 
                                                                                                                                 notification.Surname, 
                                                                                                                                 notification.Email, 
                                                                                                                                 notification.Document, 
                                                                                                                                 notification.Birthday));

            if (!response.ValidationResult.IsValid)
            {
                _notificationService.AddNotifications(response.ValidationResult);                
                await _mediator.Send(new DeleteUserByEmailCommand(notification.Email));
            }
        }

        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
