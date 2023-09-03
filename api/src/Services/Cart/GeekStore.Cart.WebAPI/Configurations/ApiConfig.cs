using GeekStore.WebApi.Core.User;
using GeekStore.WebApi.Core.Identity;
using GeekStore.Core.DI;
using GeekStore.Cart.Data.DI;
using GeekStore.Cart.Application.DI;
using GeekStore.WebApi.Core.Middlewares;
using GeekStore.Core.Extensions;

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

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

            });

            services.AddLogging(builder =>
            {
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information);
            });

            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseCors("Total");

            app.UseRouting();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthConfiguration();
            app.UseJwksDiscovery();

            return app;
        }
    }
}