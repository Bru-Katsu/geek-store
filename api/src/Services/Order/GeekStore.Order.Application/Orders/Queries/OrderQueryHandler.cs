using GeekStore.Core.Data.Extensions;
using GeekStore.Core.Models;
using GeekStore.Order.Application.Orders.ViewModels;
using GeekStore.Order.Domain.Orders.Repositories;
using MediatR;

namespace GeekStore.Order.Application.Orders.Queries
{
    public class OrderQueryHandler : IRequestHandler<OrderQuery, OrderViewModel>,
                                     IRequestHandler<OrdersListQuery, Page<OrderListViewModel>>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId);

            return new OrderViewModel
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Address = new DTOs.AddressDTO(order.Address.Street, order.Address.City, order.Address.State, order.Address.Country, order.Address.ZipCode),
                Coupon = new DTOs.CouponDTO(order.Coupon.CouponCode, order.Coupon.DiscountAmount),
                Items = order.OrderItems.Select(i => new DTOs.OrderItemDTO(i.ProductId, i.ProductName, i.ProductImage, i.Quantity, i.Price)),
                Total = order.Total,
                TotalDiscount = order.TotalDiscount,
            };
        }

        public async Task<Page<OrderListViewModel>> Handle(OrdersListQuery request, CancellationToken cancellationToken)
        {
            var ordersQuery = _orderRepository.AsQueryable();

            if(request.UserId.HasValue)
                ordersQuery = ordersQuery.Where(order => order.UserId == request.UserId);

            return await ordersQuery.AsPagedAsync((entity) => new OrderListViewModel
            {
                Id = entity.Id,
                CreationDate = entity.CreationDate,
                ItemsCount = entity.OrderItems.Count(),
                Total = entity.Total,
                TotalDiscount = entity.TotalDiscount
            }, request.PageIndex, request.PageSize);
        }
    }
}
