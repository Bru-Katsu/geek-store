using GeekStore.Cart.Application.Cart.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Cart.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCartApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddProductToCartCommand>, CartCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyCouponOnCartCommand>, CartCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductFromCartCommand>, CartCommandHandler>();

            return services;
        }
    }
}
