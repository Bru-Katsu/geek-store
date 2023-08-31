using GeekStore.WebApi.Core.User;
using GeekStore.Core.DI;
using GeekStore.WebApi.Core.Middlewares;
using GeekStore.Customer.Data.DI;
using GeekStore.Customer.Application.DI;
using GeekStore.WebApi.Core.Identity;
using GeekStore.MessageBus;

namespace GeekStore.Customer.WebAPI.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration)
                    .AddEndpointsApiExplorer();

            services.AddCoreServices()
                    .AddCustomerDataServices()
                    .AddCustomerApplicationServices()
                    .AddBackgroundServices()
                    .AddMessageBus(configuration.GetConnectionString("MessageBusConnection"))
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

        public static WebApplication UseApiConfiguration(this WebApplication app)
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
