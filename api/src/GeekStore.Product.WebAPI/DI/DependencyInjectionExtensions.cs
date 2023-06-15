using GeekStore.Product.Application.Products.Commands;
using MediatR;

namespace GeekStore.Product.WebAPI.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddProductCommand>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductCommand>, ProductCommandHandler>();            

            return services;
        }
    }
}
