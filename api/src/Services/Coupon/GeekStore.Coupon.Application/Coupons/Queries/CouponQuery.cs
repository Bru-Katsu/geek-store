using GeekStore.Core.Queries;
using GeekStore.Coupon.Application.Coupons.ViewModels;

namespace GeekStore.Coupon.Application.Coupons.Queries
{
    public class CouponQuery : IQuery<CouponViewModel>
    {
        public string CouponCode { get; set; }
    }
}
