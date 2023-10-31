using GeekStore.Website.Gateway.WebAPI.Services.Cache;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.Extensions.Caching.Distributed;
using GeekStore.Core.Extensions;

namespace GeekStore.Website.Gateway.WebAPI.Data.CacheServices
{
    public class CustomerCacheService : ICustomerCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ICustomerApiService _customerApiService;

        public CustomerCacheService(IDistributedCache cache, ICustomerApiService customerApiService)
        {
            _cache = cache;
            _customerApiService = customerApiService;
        }

        public async Task<Guid> GetCustomerId(Guid userId)
        {
            var customerId = await _cache.GetAsync<Guid?>(userId.ToString());
            if(customerId == null)
            {
                var customer = await _customerApiService.GetCustomerByUser(userId);
                await _cache.SetAsync(userId.ToString(), customer.Id);
                customerId = customer.Id;
            }

            return customerId.Value;
        }
    }
}
