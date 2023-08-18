using GeekStore.Core.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;

namespace GeekStore.Customer.Application.Customers.Queries
{
    public class CustomerListQuery : QueryPaged<CustomerListViewModel>
    {
        public string Name { get; set; }
        public string Document { get; set; }

    }
}
