using GeekStore.Core.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Core.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();           
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));

            return services;
        }
    }
}
