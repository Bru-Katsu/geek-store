using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.Models;
using GeekStore.WebApi.Core.User;
using GeekStore.Website.Gateway.WebAPI.Models.Cart;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : MainController
    {
        private readonly ICartApiService _cartApi;
        private readonly ICouponApiService _couponApi;
        private readonly IProductApiService _productApi;
        private readonly IAspNetUser _user;
        public CartController(INotificationService notificationService, ICartApiService cartApi, ICouponApiService couponApi, IProductApiService productApi, IAspNetUser user) : base(notificationService)
        {
            _cartApi = cartApi;
            _couponApi = couponApi;
            _productApi = productApi;
            _user = user;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart()
        {
            var userId = _user.GetUserId();
            var response = await _cartApi.GetCart(userId);
            return Ok(response);
        }

        //TODO: mudar para receber código string
        [HttpPut("coupon/{couponId}")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApplyCoupon([FromRoute] Guid couponId)
        {
            var userId = _user.GetUserId();
            var coupon = await _couponApi.GetCoupon(couponId);
            if(coupon is null)
            {
                AddValidationError("Cupom inválido!");
                return CreateResponse();
            }

            var response = await _cartApi.ApplyCoupon(userId, couponId);
            return Ok(response);
        }

        [HttpPost("items")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductIntoCart([FromBody] AddProductToCartRequest request)
        {
            var userId = _user.GetUserId();
            var product = await _productApi.GetProduct(request.ProductId);
            if(product is null)
            {
                AddValidationError("Produto inexistente!");
                return CreateResponse();
            }

            var response = await _cartApi.AddProduct(userId, new AddProductInternalRequest
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = request.Quantity,
                Price = product.Price
            });

            return Ok(response);
        }

        [HttpDelete("items/{productId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] Guid productId)
        {
            var userId = _user.GetUserId();
            var response = await _cartApi.DeleteProduct(userId, productId);
            return Ok(response);
        }
    }
}
