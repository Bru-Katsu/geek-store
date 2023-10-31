using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.Order.Application.Orders.Commands;
using GeekStore.Order.Application.Orders.DTOs;
using GeekStore.Order.Application.Orders.Queries;
using GeekStore.Order.Application.Orders.ViewModels;
using GeekStore.Order.WebAPI.Requests;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.Models;
using GeekStore.WebApi.Core.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : MainController
    {
        private readonly IMediator _mediator;
        public OrderController(INotificationService notificationService, 
                               IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId}")]
        [Authorize]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _mediator.Send(new OrderQuery()
            {
                OrderId = orderId
            });

            if(order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("{userId}/all")]
        [Authorize]
        [ProducesResponseType(typeof(Page<OrderListViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders([FromRoute] Guid userId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var orders = await _mediator.Send(new OrdersListQuery()
            {
                UserId = userId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, [FromServices] IAspNetUser user)
        {
            var userId = user.GetUserId();
            var items = request.Items.Select(i => new OrderItemDTO(i.ProductId, i.ProductName, i.ProductImage, i.Quantity, i.Price));

            var couponDto = default(CouponDTO);
            var addressDto = new AddressDTO(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode);

            if (request.Coupon != null)
                couponDto = new CouponDTO(request.Coupon.CouponCode, request.Coupon.DiscountAmount);

            var command = new CreateOrderCommand(userId, addressDto, couponDto, items);

            var result = await _mediator.Send(command);

            if(!result.Succeeded)
                return CreateResponse();

            var order = await _mediator.Send(new OrderQuery
            {
                OrderId = result.Data
            });

            return Ok(order);
        }
    }
}
