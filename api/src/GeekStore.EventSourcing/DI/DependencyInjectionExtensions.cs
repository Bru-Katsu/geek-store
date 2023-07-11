using GeekStore.Core.DI;
using GeekStore.Core.EventSourcing.Repositories;
using GeekStore.EventSourcing.Repositories;
using GeekStore.EventSourcing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.EventSourcing.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services)
        {
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
            services.UseCustomMediator();

            return services;
        }
    }
}
