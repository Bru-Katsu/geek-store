using MediatR;

namespace GeekStore.Customer.Application.Addresses.Events
{
    public class CustomerAddressEventHandler : INotificationHandler<AddressAddedEvent>
    {
        public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
