using GeekStore.WebApi.Core.User;
using GeekStore.WebApi.Core.Identity;
using GeekStore.EventSourcing.DI;
using GeekStore.Product.Application.DI;
using GeekStore.Core.DI;
using GeekStore.Product.Data.Configurations;

namespace GeekStore.Product.WebAPI.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddCoreServices()
                    .AddProductDataServices()
                    .AddProductApplicationServices()
                    .AddEventSourcing()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthConfiguration();
            app.UseJwksDiscovery();

            return app;
        }
    }
}