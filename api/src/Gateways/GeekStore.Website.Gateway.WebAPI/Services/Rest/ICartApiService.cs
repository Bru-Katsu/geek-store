using GeekStore.Website.Gateway.WebAPI.Models.Cart;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface ICartApiService
    {
        [Get("/cart/{id}")]
        Task<CartResponse> GetCart([AliasAs("id")] Guid userId);

        [Put("/cart/{userId}/coupon/{couponId}")]
        Task<CartResponse> ApplyCoupon([AliasAs("userId")] Guid userId, [AliasAs("couponId")] Guid couponId);
        
        [Post("/cart/{userId}/items")]
        Task<CartResponse> AddProduct([AliasAs("userId")] Guid userId, [Body] AddProductInternalRequest request);
        
        [Delete("/cart/{userId}/items/{productId}")]
        Task<CartResponse> DeleteProduct([AliasAs("userId")] Guid userId, [AliasAs("productId")] Guid productId);
    }
}
