using MediatR;

namespace GeekStore.Customer.Application.Customers.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerCreatedEvent>,
                                        INotificationHandler<CustomerProfileImageChangedEvent>
    {
        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(CustomerProfileImageChangedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
