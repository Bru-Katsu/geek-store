using GeekStore.WebApi.Core.User;
using GeekStore.WebApi.Core.Identity;
using GeekStore.EventSourcing.DI;
using GeekStore.Product.Application.DI;
using GeekStore.Core.DI;
using GeekStore.Product.Data.Configurations;
using GeekStore.WebApi.Core.Middlewares;
using GeekStore.Product.WebAPI.Configuration;

namespace GeekStore.Product.WebAPI.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration)
                    .AddEndpointsApiExplorer();

            services.AddCoreServices()
                    .AddProductDataServices()
                    .AddProductApplicationServices()
                    .AddEventSourcing()
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