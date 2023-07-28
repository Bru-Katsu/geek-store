using GeekStore.Coupon.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Coupon.Data.DI
{
    public static class EFConfiguration
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<CouponDataContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
