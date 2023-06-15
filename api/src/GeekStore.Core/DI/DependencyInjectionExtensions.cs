using GeekStore.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Core.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
