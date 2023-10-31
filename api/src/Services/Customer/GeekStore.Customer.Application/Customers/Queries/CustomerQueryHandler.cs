using GeekStore.Core.Data.Extensions;
using GeekStore.Core.Models;
using GeekStore.Customer.Application.Customers.ViewModels;
using GeekStore.Customer.Domain.Customers.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Customer.Application.Customers.Queries
{
    public class CustomerQueryHandler : IRequestHandler<CustomerQuery, CustomerViewModel>,
                                        IRequestHandler<CustomerListQuery, Page<CustomerListViewModel>>,
                                        IRequestHandler<CustomerByUserQuery, CustomerViewModel>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerViewModel> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.Id);

            if (customer is null)
                return default;

            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Birthday = customer.Birthday,
                Document = customer.Document,
            };            
        }

        public async Task<Page<CustomerListViewModel>> Handle(CustomerListQuery request, CancellationToken cancellationToken)
        {
            var query = _customerRepository.AsQueryable();

            if(!string.IsNullOrEmpty(request.Name))
                query = query.Where(x => x.Name.Contains(request.Name));

            if(!string.IsNullOrEmpty(request.Document))
                query = query.Where(x => x.Document.Contains(request.Document));

            return await query.AsPagedAsync((entity) => new CustomerListViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
            }, request.PageIndex, request.PageSize);            
        }

        public async Task<CustomerViewModel> Handle(CustomerByUserQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.AsQueryable().FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (customer is null)
                return default;

            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Birthday = customer.Birthday,
                Document = customer.Document,
            };
        }
    }
}
