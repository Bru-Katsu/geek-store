using GeekStore.Core.Models;
using GeekStore.Core.Results;
using GeekStore.Order.Application.Orders.Commands;
using GeekStore.Order.Application.Orders.Events;
using GeekStore.Order.Application.Orders.Queries;
using GeekStore.Order.Application.Orders.ViewModels;
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

            services.AddScoped<IRequestHandler<OrderQuery, OrderViewModel>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<OrdersListQuery, Page<OrderListViewModel>>, OrderQueryHandler>();

            return services;
        }
    }
}
