using GeekStore.Cart.Application.Cart.Queries;
using GeekStore.Cart.Application.Cart.ViewModels;
using GeekStore.Cart.Data.DTOs;
using GeekStore.Cart.Data.Factories;
using GeekStore.Cart.Data.QueryHandlers;
using GeekStore.Cart.Data.Repositories;
using GeekStore.Cart.Domain.Carts;
using GeekStore.Cart.Domain.Carts.Repositories;
using GeekStore.Core.Factories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Cart.Data.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCartDataServices(this IServiceCollection services, string redisConnection)
        {
            services.AddScoped<IRequestHandler<CartQuery, CartViewModel>, CartQueryHandler>();
            services.AddScoped<IEntityFactory<CartItem, CartItemDTO>, CartItemFactory>();
            services.AddScoped<IEntityFactory<Domain.Carts.Cart, CartDTO>, CartFactory>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });

            return services;
        }
    }
}
