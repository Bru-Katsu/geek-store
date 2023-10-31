using GeekStore.WebApi.Core.User;
using GeekStore.Core.DI;
using MediatR;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using GeekStore.Website.Gateway.WebAPI.Behaviors;
using GeekStore.Website.Gateway.WebAPI.Extensions;
using GeekStore.Website.Gateway.WebAPI.Common;
using Polly;
using Refit;
using System.Collections.ObjectModel;
using GeekStore.Website.Gateway.WebAPI.Middlewares;
using GeekStore.Website.Gateway.WebAPI.Services.Cache;
using GeekStore.Website.Gateway.WebAPI.Data.CacheServices;

namespace GeekStore.Website.Gateway.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddConfigurationServices();
            services.AddCoreServices();
            services.AddApiServices(configuration);
            services.AddPipelines();
            services.AddMiddlewares();
            services.AddRedis(configuration);
            services.AddCacheServices();

            return services;
        }

        private static IServiceCollection AddConfigurationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddTransient<AuthorizationDelegatingHandler>();

            return services;
        }

        private static IServiceCollection AddCacheServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerCacheService, CustomerCacheService>();
            return services;
        }

        private static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiServices = new Collection<(Type ServiceType, string AddressConfigurated)>
            {
                { (typeof(IIdentityApiService), "ServicesSettings:IdentityApi") },
                { (typeof(IProductApiService), "ServicesSettings:ProductApi") },
                { (typeof(IOrderApiService), "ServicesSettings:OrderApi") },
                { (typeof(ICustomerApiService), "ServicesSettings:CustomerApi") },
                { (typeof(ICouponApiService), "ServicesSettings:CouponApi") },
                { (typeof(ICartApiService), "ServicesSettings:CartApi") },
            };

            foreach (var (ServiceType, AddressConfigurated) in apiServices)
            {
                services
                    .AddRefitClient(ServiceType)
                    .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration[AddressConfigurated]))
                    .AddHttpMessageHandler<AuthorizationDelegatingHandler>()
                    .AllowSelfSignedCertificate()
                    .AddPolicyHandler(Policies.TryWait())
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            }
            
            return services;
        }

        private static IServiceCollection AddPipelines(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));

            return services;
        }

        private static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();

            return services;
        }
    }
}