using GeekStore.Core.Queries;
using GeekStore.Customer.Application.Addresses.ViewModels;

namespace GeekStore.Customer.Application.Addresses.Queries
{
    public class CustomerAddressesQuery : QueryPaged<CustomerAddressViewModel>
    {
        public Guid CustomerId { get; set; }
    }
}
