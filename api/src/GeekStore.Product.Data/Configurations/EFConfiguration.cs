using GeekStore.Product.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Product.Data.Configurations
{
    public static class EFConfiguration
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<ProductDataContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
