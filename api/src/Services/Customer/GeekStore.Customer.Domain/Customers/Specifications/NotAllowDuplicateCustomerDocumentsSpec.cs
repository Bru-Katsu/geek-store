using GeekStore.Core.Specification;
using GeekStore.Customer.Domain.Customers.Repositories;

namespace GeekStore.Customer.Domain.Customers.Specifications
{
    public class NotAllowDuplicateCustomerDocumentsSpec : IAsyncSpecification<Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public NotAllowDuplicateCustomerDocumentsSpec(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> IsSatisfiedBy(Customer entity)
        {
            var customers = await _customerRepository.Filter<Customer>(c => c.Document == entity.Document);
            return !customers.Any();
        }
    }
}
