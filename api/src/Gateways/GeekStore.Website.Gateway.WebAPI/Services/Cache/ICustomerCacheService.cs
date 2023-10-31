using GeekStore.Website.Gateway.WebAPI.Services.Rest;

namespace GeekStore.Website.Gateway.WebAPI.Services.Cache
{
    public interface ICustomerCacheService
    {
        public Task<Guid> GetCustomerId(Guid userId);
    }
}
