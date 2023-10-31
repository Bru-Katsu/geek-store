using GeekStore.Core.Data.Extensions;
using GeekStore.Core.Models;
using GeekStore.Customer.Application.Addresses.ViewModels;
using GeekStore.Customer.Domain.Customers.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Customer.Application.Addresses.Queries
{
    public class CustomerAddressQueryHandler : IRequestHandler<CustomerAddressQuery, CustomerAddressViewModel>,
                                               IRequestHandler<CustomerAddressesQuery, Page<CustomerAddressViewModel>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAddressQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerAddressViewModel> Handle(CustomerAddressQuery request, CancellationToken cancellationToken)
        {
            var entity = await _customerRepository
                .AsQueryable()
                .SelectMany(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.Id == request.AddressId);

            if (entity is null)
                return default;

            return new CustomerAddressViewModel
            {
                Id = entity.Id,
                Street = entity.Street,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                ZipCode = entity.ZipCode,
                Type = (int)entity.Type,
            };            
        }

        public async Task<Page<CustomerAddressViewModel>> Handle(CustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            var query = _customerRepository
                .AsQueryable()
                .SelectMany(x => x.Addresses)
                .Where(x => x.CustomerId == request.CustomerId);

            return await query.AsPagedAsync((entity) => new CustomerAddressViewModel
            {
                Id = entity.Id,
                Street = entity.Street,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                ZipCode = entity.ZipCode,
                Type = (int)entity.Type,
            }, request.PageIndex, request.PageSize);
        }
    }
}
