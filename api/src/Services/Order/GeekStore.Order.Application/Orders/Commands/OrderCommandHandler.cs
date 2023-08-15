using GeekStore.Core.Notifications;
using GeekStore.Core.Results;
using GeekStore.Order.Application.Orders.Events;
using GeekStore.Order.Domain.Orders;
using GeekStore.Order.Domain.Orders.Repositories;
using MediatR;

namespace GeekStore.Order.Application.Orders.Commands
{
    public class OrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        private readonly INotificationService _notificationService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderCommandHandler(INotificationService notificationService,
                                   IOrderRepository orderRepository,
                                   IMediator mediator)
        {
            _notificationService = notificationService;
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return new FailResult<Guid>();
            }

            var items = request.Items.Select(x => new OrderItem(x.ProductId, x.ProductName, x.ProductImage, x.Quantity, x.Price));
            var invalidItems = items.Where(e => !e.IsValid());

            if (invalidItems.Any())
            {
                foreach (var validation in invalidItems.Select(x => x.ValidationResult))
                    _notificationService.AddNotifications(validation);

                return new FailResult<Guid>();
            }

            var entity = new Domain.Orders.Order(request.UserId, items);
            var address = new Domain.Orders.ValueObjects.Address(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode);

            entity.DefineAddress(address);

            if(request.Coupon != null)
            {
                var coupon = new Domain.Orders.ValueObjects.Coupon(request.Coupon.CouponCode, request.Coupon.DiscountAmount);
                entity.ApplyCoupon(coupon);
            }

            entity.CalculateAmount();

            if (!entity.IsValid())
            {
                _notificationService.AddNotifications(entity.ValidationResult);
                return new FailResult<Guid>();
            }

            _orderRepository.Insert(entity);
            await _mediator.Publish(new OrderCreatedEvent(entity));

            return new SuccessResult<Guid>(entity.Id);
        }
    }
}
