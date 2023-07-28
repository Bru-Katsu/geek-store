using MediatR;

namespace GeekStore.Product.Application.Products.Events
{
    internal class ProductEventHandler : INotificationHandler<ProductAddedEvent>,
                                         INotificationHandler<ProductUpdatedEvent>,
                                         INotificationHandler<ProductRemovedEvent>
    {
        public Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProductRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
