using GeekStore.Core.Mediator;
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
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
            services.AddScoped<IMediator, MediatorDecorator>();

            return services;
        }
    }
}
