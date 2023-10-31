using GeekStore.Core.Models;
using GeekStore.Website.Gateway.WebAPI.Models.Product;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface IProductApiService
    {
        [Get("/product/{id}")]
        Task<ProductResponse> GetProduct([AliasAs("id")] Guid productId);

        [Get("/product/all/")]
        Task<Page<ProductResponse>> GetAllProducts([Query] AllProductsPageRequest request);

        [Post("/product")]
        Task<ProductResponse> AddProduct([Body] AddProductRequest request);

        [Put("/product")]
        Task<ProductResponse> UpdateProduct([Body] UpdateProductRequest request);

        [Delete("/product/{id}")]
        Task DeleteProduct([AliasAs("id")] Guid productId);
    }
}
