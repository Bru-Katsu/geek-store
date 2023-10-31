using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.User;
using GeekStore.Website.Gateway.WebAPI.Models.Order;
using GeekStore.Website.Gateway.WebAPI.Services.Cache;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : MainController
    {
        private readonly IOrderApiService _orderApi;
        private readonly ICartApiService _cartApi;
        private readonly IProductApiService _productApi;
        private readonly ICustomerApiService _customerApi;
        private readonly ICouponApiService _couponApi;
        private readonly IAspNetUser _aspNetUser;
        private readonly ICustomerCacheService _customerCache;
        public OrderController(INotificationService notificationService,
                                  ICartApiService cartApi,
                                  IProductApiService productApi,
                                  IOrderApiService orderApi,
                                  ICustomerApiService customerApi,
                                  ICouponApiService couponApi,
                                  IAspNetUser aspNetUser,
                                  ICustomerCacheService customerCache) : base(notificationService)
        {
            _cartApi = cartApi;
            _productApi = productApi;
            _customerApi = customerApi;
            _couponApi = couponApi;
            _aspNetUser = aspNetUser;
            _customerCache = customerCache;
            _orderApi = orderApi;
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var response = await _orderApi.GetOrder(orderId);
            return Ok(response);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(Page<OrdersResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var userId = _aspNetUser.GetUserId();
            var response = _orderApi.GetAllOrdersByUser(userId, pageIndex, pageSize);
            return Ok(response);
        }

        [HttpPost("checkout")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = _aspNetUser.GetUserId();

            var cart = await _cartApi.GetCart(userId);
            var customerId = await _customerCache.GetCustomerId(userId);
            var address = await _customerApi.GetAddress(customerId, request.AddressId);

            var items = new List<ItemOrderInternalRequest>();
            foreach (var item in cart.Items)
            {
                var product = await _productApi.GetProduct(item.ProductId);
                items.Add(new ItemOrderInternalRequest
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImage = product.ImageURL,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            var orderRequest = new CreateOrderInternalRequest
            {
                Items = items,
                Address = new AddressOrderInternalRequest
                {
                    Street = address.Street,
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    ZipCode = address.ZipCode,
                },
            };

            if (cart.CouponId.HasValue)
            {
                var coupon = await _couponApi.GetCoupon(cart.CouponId.Value);
                orderRequest.Coupon = new CouponOrderInternalRequest
                {
                    CouponCode = coupon.CouponCode,
                    DiscountAmount = coupon.DiscountAmount
                };
            }

            var response = await _orderApi.CreateOrder(orderRequest);
            return Ok(response);
        }
    }
}
