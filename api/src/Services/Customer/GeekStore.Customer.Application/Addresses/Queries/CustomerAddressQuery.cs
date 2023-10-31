using GeekStore.Core.Queries;
using GeekStore.Customer.Application.Addresses.ViewModels;

namespace GeekStore.Customer.Application.Addresses.Queries
{
    public class CustomerAddressQuery : IQuery<CustomerAddressViewModel>
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
    }
}
