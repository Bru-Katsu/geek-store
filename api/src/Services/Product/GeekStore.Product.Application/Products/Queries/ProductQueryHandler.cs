using GeekStore.Core.Models;
using GeekStore.Product.Application.Products.ViewModels;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using GeekStore.Core.Data.Extensions;

namespace GeekStore.Product.Application.Products.Queries
{
    public class ProductQueryHandler : IRequestHandler<ProductListQuery, Page<ProductsViewModel>>,
                                       IRequestHandler<ProductQuery, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Page<ProductsViewModel>> Handle(ProductListQuery request, CancellationToken cancellationToken)
        {
            var query = _productRepository.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(x => x.Name.Contains(request.Name));

            return await query.AsPagedAsync(factory: (entity) => new ProductsViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price
            }, request.PageIndex, request.PageSize);
        }

        public async Task<ProductViewModel> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetById(request.Id);
            
            if (entity is null)
                return default;

            return new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Category = entity.Category,
                ImageURL = entity.ImageURL,
                Description = entity.Description
            };
        }
    }
}
