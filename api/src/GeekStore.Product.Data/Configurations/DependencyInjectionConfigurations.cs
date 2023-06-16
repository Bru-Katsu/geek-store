using GeekStore.Core.Models;
using GeekStore.Product.Application.Products.Queries;
using GeekStore.Product.Application.Products.ViewModels;
using GeekStore.Product.Data.Context;
using GeekStore.Product.Data.QueryHandlers;
using GeekStore.Product.Data.Repositories;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Product.Data.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ProductDataContext>(provider => provider.GetService<ProductDataContext>());
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IRequestHandler<ProductQuery, ProductViewModel>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<ProductListQuery, Page<ProductsViewModel>>, ProductQueryHandler>();

            return services;
        }
    }
}
