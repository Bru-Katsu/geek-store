using GeekStore.Core.Models;
using GeekStore.Core.UoW;
using GeekStore.Product.Application.Products.Queries;
using GeekStore.Product.Application.Products.ViewModels;
using GeekStore.Product.Data.QueryHandlers;
using GeekStore.Product.Data.Repositories;
using GeekStore.Product.Data.UoW;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Product.Data.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, ProductUnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IRequestHandler<ProductQuery, ProductViewModel>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<ProductListQuery, Page<ProductsViewModel>>, ProductQueryHandler>();

            return services;
        }
    }
}
