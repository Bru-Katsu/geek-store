using GeekStore.Coupon.Data.Repositories;
using GeekStore.Coupon.Domain.Coupons.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Coupon.Data.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCouponDataServices(this IServiceCollection services)
        {
            services.AddScoped<ICouponRepository, CouponRepository>();
            
            return services;
        }
    }
}
