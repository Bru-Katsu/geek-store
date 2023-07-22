using GeekStore.WebApi.Core.User;
using GeekStore.WebApi.Core.Identity;
using GeekStore.Core.DI;
using GeekStore.Cart.Data.DI;
using GeekStore.Cart.Application.DI;
using GeekStore.Core.Helpers;
using GeekStore.WebApi.Core.Middlewares;

namespace GeekStore.Cart.WebAPI.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            services.AddCoreServices()
                    .AddCartDataServices(configuration.GetCaching("RedisAddress"))
                    .AddCartApplicationServices()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseCors(options =>
            {
                options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200");
            });

            app.UseRouting();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthConfiguration();
            app.UseJwksDiscovery();

            return app;
        }
    }
}