using GeekStore.Core.Notifications;
using GeekStore.Coupon.Application.Coupons.Queries;
using GeekStore.Coupon.Application.Coupons.ViewModels;
using GeekStore.WebApi.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Coupon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CouponController : MainController
    {
        private readonly IMediator _mediator;

        public CouponController(INotificationService notificationService, 
                                IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{couponCode}")]
        [ProducesResponseType(typeof(CouponViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCoupon([FromRoute] string couponCode)
        {
            var result = await _mediator.Send(new CouponQuery()
            {
                CouponCode = couponCode
            });

            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
