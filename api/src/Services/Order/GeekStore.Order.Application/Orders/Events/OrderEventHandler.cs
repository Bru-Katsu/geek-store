using MediatR;

namespace GeekStore.Order.Application.Orders.Events
{
    public class OrderEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
