using GeekStore.Core.Specification;
using GeekStore.Customer.Domain.Customers.Repositories;
using GeekStore.Customer.Domain.Customers.Specifications;

namespace GeekStore.Customer.Domain.Customers.Validators
{
    public class CustomerAddValidator : AsyncValidator<Customer>
    {
        public CustomerAddValidator(ICustomerRepository customerRepository)
        {
            Add(new NotAllowDuplicateCustomerDocumentsSpec(customerRepository), "Um usuário já está associado a este documento de identificação");
        }
    }
}
