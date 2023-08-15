using GeekStore.Order.Data.Context;
using GeekStore.Order.Data.Repositories;
using GeekStore.Order.Domain.Orders.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Order.Data.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddOrderDataServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<OrderDataContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
