using GeekStore.Core.Models;
using GeekStore.Core.Results;
using GeekStore.Customer.Application.Customers.Commands;
using GeekStore.Customer.Application.Customers.Events;
using GeekStore.Customer.Application.Customers.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Customer.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomerApplicationServices(this IServiceCollection services)
        {
            //command
            services.AddScoped<IRequestHandler<CreateCustomerCommand, Result<Guid>>, CustomerCommandHandler>();

            //events
            services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();

            //queries
            services.AddScoped<IRequestHandler<CustomerQuery, CustomerViewModel>, CustomerQueryHandler>();
            services.AddScoped<IRequestHandler<CustomerListQuery, Page<CustomerListViewModel>>, CustomerQueryHandler>();

            return services;
        }
    }
}
