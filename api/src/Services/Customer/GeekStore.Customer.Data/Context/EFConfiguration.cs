using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Customer.Data.Context
{
    public static class EFConfiguration
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<CustomerDataContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
