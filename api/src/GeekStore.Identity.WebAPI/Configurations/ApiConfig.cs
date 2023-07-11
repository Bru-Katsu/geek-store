using GeekStore.WebApi.Core.Identity;
using GeekStore.WebApi.Core.Middlewares;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();

            services.AddIdentityConfiguration(Configuration);

            services.AddDependencyInjectionConfiguration();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerConfiguration();

            return services;
        }

        public static WebApplication UseApplicationConfiguration(this WebApplication app)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthConfiguration();
            
            app.UseJwksDiscovery();

            app.MapControllers();

            return app;
        }
    }
}
