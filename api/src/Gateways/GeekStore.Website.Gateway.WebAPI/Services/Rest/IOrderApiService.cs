using GeekStore.Core.Models;
using GeekStore.Website.Gateway.WebAPI.Models.Order;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface IOrderApiService
    {
        [Get("/order/{orderId}")]
        Task<OrderResponse> GetOrder([AliasAs("orderId")] Guid orderId);

        [Get("/order/{userId}/all")]
        Task<Page<OrdersResponse>> GetAllOrdersByUser([AliasAs("userId")] Guid userId, [Query] int pageIndex, [Query] int pageSize);

        [Post("/order")]
        Task<OrderResponse> CreateOrder(CreateOrderInternalRequest request);
    }
}
