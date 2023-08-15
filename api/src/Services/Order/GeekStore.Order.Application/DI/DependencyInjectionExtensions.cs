using GeekStore.Core.Results;
using GeekStore.Order.Application.Orders.Commands;
using GeekStore.Order.Application.Orders.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Order.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddOrderApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateOrderCommand, Result<Guid>>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<OrderCreatedEvent>, OrderEventHandler>();

            return services;
        }
    }
}
