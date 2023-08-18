using MediatR;

namespace GeekStore.Customer.Application.Customers.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
