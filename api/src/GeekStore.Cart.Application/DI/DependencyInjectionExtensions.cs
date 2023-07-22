using GeekStore.Cart.Application.Cart.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Cart.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCartApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddProductCartCommand>, CartCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyCouponCartCommand>, CartCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductCartCommand>, CartCommandHandler>();

            return services;
        }
    }
}
