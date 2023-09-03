using GeekStore.Core.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;

namespace GeekStore.Customer.Application.Customers.Queries
{
    public class CustomerQuery : IQuery<CustomerViewModel>
    {
        public Guid Id { get; set; }
    }
}
