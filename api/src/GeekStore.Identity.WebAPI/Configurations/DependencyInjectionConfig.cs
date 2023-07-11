using GeekStore.Identity.Application.DI;
using GeekStore.Identity.Data.DI;
using GeekStore.EventSourcing.DI;
using GeekStore.WebApi.Core.User;
using GeekStore.Identity.Domain.DI;
using GeekStore.Core.DI;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCoreServices()
                    .AddIdentityApplicationServices()
                    .AddIdentityDataServices()
                    .AddIdentityDomainServices()
                    .AddEventSourcing()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            return services;
        }
    }
}