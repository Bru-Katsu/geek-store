using GeekStore.Coupon.Application.Coupons.ViewModels;
using GeekStore.Coupon.Domain.Coupons.Repositories;
using MediatR;

namespace GeekStore.Coupon.Application.Coupons.Queries
{
    public class CouponQueryHandler : IRequestHandler<CouponQuery, CouponViewModel>
    {
        private readonly ICouponRepository _couponRepository;

        public CouponQueryHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<CouponViewModel> Handle(CouponQuery request, CancellationToken cancellationToken)
        {
            var entities = await _couponRepository.Filter(e => e.CouponCode == request.CouponCode);
            var coupon = entities.First();

            return new CouponViewModel
            {
                Id = coupon.Id,
                CouponCode = coupon.CouponCode,
                DiscountAmount = coupon.DiscountAmount,
            };
        }
    }
}
