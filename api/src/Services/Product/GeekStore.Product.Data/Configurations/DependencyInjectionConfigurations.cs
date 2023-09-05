using GeekStore.Core.Models;
using GeekStore.Product.Application.Products.Queries;
using GeekStore.Product.Application.Products.ViewModels;
using GeekStore.Product.Data.Context;
using GeekStore.Product.Data.Repositories;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Product.Data.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddProductDataServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ProductDataContext>(provider => provider.GetService<ProductDataContext>());
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
