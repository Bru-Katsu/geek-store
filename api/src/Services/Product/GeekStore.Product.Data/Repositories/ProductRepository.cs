using GeekStore.Core.Data;
using GeekStore.Product.Data.Context;
using GeekStore.Product.Domain.Products.Repositories;

namespace GeekStore.Product.Data.Repositories
{
    public class ProductRepository : Repository<ProductDataContext, Domain.Products.Product>, IProductRepository
    {
        public ProductRepository(ProductDataContext context) : base(context) { }
    }
}
