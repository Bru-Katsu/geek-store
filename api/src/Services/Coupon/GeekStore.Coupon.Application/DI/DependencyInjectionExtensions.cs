using GeekStore.Coupon.Application.Coupons.Queries;
using GeekStore.Coupon.Application.Coupons.ViewModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Coupon.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCouponApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CouponQuery, CouponViewModel>, CouponQueryHandler>();
            
            return services;
        }
    }
}
