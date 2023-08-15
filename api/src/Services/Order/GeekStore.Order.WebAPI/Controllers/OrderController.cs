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
        private readonly IAspNetUser _aspNetUser;
        private readonly IMediator _mediator;
        public OrderController(INotificationService notificationService, 
                               IAspNetUser aspNetUser, 
                               IMediator mediator) : base(notificationService)
        {
            _aspNetUser = aspNetUser;
            _mediator = mediator;
        }

        [HttpGet("{guid:orderId}")]
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

        [HttpGet("{guid:userId}/all")]
        [Authorize]
        [ProducesResponseType(typeof(OrderListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders(Guid userId)
        {
            var orders = await _mediator.Send(new OrdersListQuery()
            {
                UserId = userId
            });

            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = _aspNetUser.GetUserId();
            var items = request.Items.Select(i => new OrderItemDTO(i.ProductId, i.ProductName, i.ProductImage, i.Quantity, i.Price));

            var command = new CreateOrderCommand(userId, 
                                                 new AddressDTO(request.Street, request.City, request.State, request.Country, request.ZipCode), 
                                                 new CouponDTO(request.CouponCode, request.DiscountAmount), 
                                                 items);

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
