using GeekStore.Core.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;

namespace GeekStore.Customer.Application.Customers.Queries
{
    public class CustomerByUserQuery : IQuery<CustomerViewModel>
    {
        public Guid UserId { get; set; }
    }
}
