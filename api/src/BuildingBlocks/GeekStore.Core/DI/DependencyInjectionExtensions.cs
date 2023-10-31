﻿using GeekStore.Core.Mediator;
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

            return services;
        }

        public static IServiceCollection UseCustomMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, MediatorProxy>();

            return services;
        }
    }
}
