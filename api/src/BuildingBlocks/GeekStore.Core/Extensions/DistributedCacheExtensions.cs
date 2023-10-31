using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GeekStore.Core.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            await cache.SetStringAsync(key, content);
        }
    }
}
