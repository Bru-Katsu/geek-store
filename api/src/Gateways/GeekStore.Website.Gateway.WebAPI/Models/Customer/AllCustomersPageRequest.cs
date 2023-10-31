using GeekStore.Core.Queries;
using GeekStore.Website.Gateway.WebAPI.Models.Base;

namespace GeekStore.Website.Gateway.WebAPI.Models.Customer
{
    public class AllCustomersPageRequest : PagedRequest<CustomersResponse>
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
    }
}
