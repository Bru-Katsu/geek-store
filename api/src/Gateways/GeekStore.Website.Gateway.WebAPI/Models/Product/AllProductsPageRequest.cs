using GeekStore.Core.Queries;
using GeekStore.Website.Gateway.WebAPI.Models.Base;

namespace GeekStore.Website.Gateway.WebAPI.Models.Product
{
    public class AllProductsPageRequest : PagedRequest<ProductsResponse>
    {
        public string? Name { get; set; }
    }
}
