using GeekStore.Core.Queries;
using GeekStore.Product.Application.Products.ViewModels;

namespace GeekStore.Product.Application.Products.Queries
{
    public class ProductQuery : QuerySorted<ProductViewModel>
    {
        public Guid Id { get; set; }
    }
}
