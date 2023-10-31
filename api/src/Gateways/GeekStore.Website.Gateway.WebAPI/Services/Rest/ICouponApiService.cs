using GeekStore.Website.Gateway.WebAPI.Models.Coupon;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface ICouponApiService
    {
        [Get("/coupon/{couponCode}")]
        Task<CouponResponse> GetCoupon([AliasAs("couponCode")] Guid couponId);
    }
}
