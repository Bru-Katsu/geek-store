using GeekStore.Customer.WebAPI.Services;

namespace GeekStore.Customer.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<CustomersBackgroundService>();

            return services;
        }
    }
}