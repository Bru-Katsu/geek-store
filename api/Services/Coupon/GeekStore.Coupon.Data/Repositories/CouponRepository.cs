﻿using GeekStore.Core.Data;
using GeekStore.Coupon.Data.Context;
using GeekStore.Coupon.Domain.Coupons.Repositories;

namespace GeekStore.Coupon.Data.Repositories
{
    public class CouponRepository : Repository<CouponDataContext>, ICouponRepository
    {
        public CouponRepository(CouponDataContext context) : base(context) { }
    }
}
