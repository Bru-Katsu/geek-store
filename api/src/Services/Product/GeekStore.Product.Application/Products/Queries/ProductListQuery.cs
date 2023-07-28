using GeekStore.Core.Queries;
using GeekStore.Product.Application.Products.ViewModels;

namespace GeekStore.Product.Application.Products.Queries
{
    public class ProductListQuery : QueryPaged<ProductsViewModel>
    {
        public string? Name { get; set; }
    }
}
