using GeekStore.Customer.Data.Repositories;
using GeekStore.Customer.Domain.Customers.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Customer.Data.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomerDataServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
