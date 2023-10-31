using GeekStore.Cart.Application.Cart.Commands;
using GeekStore.Cart.Application.Cart.Queries;
using GeekStore.Cart.Application.Cart.ViewModels;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Cart.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CartController : MainController
    {
        private readonly IMediator _mediator;
        public CartController(INotificationService notificationService,
                              IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart([FromRoute] Guid userId)
        {
            var response = await _mediator.Send(new CartQuery() 
            { 
                UserId = userId
            });

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{userId:guid}/coupon/{couponId}")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApplyCoupon([FromRoute] Guid userId, [FromRoute] Guid couponId)
        {
            await _mediator.Send(new ApplyCouponCartCommand(userId, couponId));

            if (HasErrors())
                return CreateResponse();

            var cart = await _mediator.Send(new CartQuery
            {
                UserId = userId,
            });

            return Ok(cart);
        }

        [Authorize]
        [HttpPost("{userId:guid}/items")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductIntoCart([FromRoute] Guid userId, [FromBody] CartItemViewModel cartItem)
        {
            await _mediator.Send(new AddProductCartCommand(userId, cartItem.ProductId, cartItem.ProductName, cartItem.Quantity, cartItem.Price));

            if (HasErrors())
                return CreateResponse();

            var cart = await _mediator.Send(new CartQuery
            {
                UserId = userId,
            });

            return Ok(cart);
        }

        [Authorize]
        [HttpDelete("{userid:guid}/items/{productId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] Guid userId, [FromRoute] Guid productId)
        {
            await _mediator.Send(new RemoveProductCartCommand(userId, productId));

            if (HasErrors())
                return CreateResponse();

            var cart = await _mediator.Send(new CartQuery
            {
                UserId = userId,
            });

            return Ok(cart);
        }
    }
}
