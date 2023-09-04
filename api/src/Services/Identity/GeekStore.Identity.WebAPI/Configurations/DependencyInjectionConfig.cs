using GeekStore.Identity.Data.DI;
using GeekStore.EventSourcing.DI;
using GeekStore.WebApi.Core.User;
using GeekStore.Identity.Domain.DI;
using GeekStore.Core.DI;
using GeekStore.MessageBus;
using GeekStore.Identity.Application;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCoreServices()
                    .AddIdentityDataServices()
                    .AddIdentityDomainServices()
                    .AddEventSourcing()
                    .AddMessageBus(configuration.GetConnectionString("MessageBusConnection"))
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationEntryPoint>());

            return services;
        }
    }
}