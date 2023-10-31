using GeekStore.WebApi.Core.Identity;
using GeekStore.Website.Gateway.WebAPI.Middlewares;

namespace GeekStore.Website.Gateway.WebAPI.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddAuthConfiguration(configuration);

            services.AddDependencyInjectionConfiguration(configuration);

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

            services.AddEndpointsApiExplorer();

            services.AddSwaggerConfiguration();

            return services;
        }

        public static WebApplication UseApplicationConfiguration(this WebApplication app)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthConfiguration();
            
            app.UseJwksDiscovery();

            app.MapControllers();

            return app;
        }
    }
}
