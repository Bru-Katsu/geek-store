using GeekStore.Product.Application.Products.Commands;
using GeekStore.Product.Application.Products.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Product.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProductApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddProductCommand>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductCommand>, ProductCommandHandler>();

            services.AddScoped<INotificationHandler<ProductAddedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductUpdatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductRemovedEvent>, ProductEventHandler>();

            return services;
        }
    }
}
