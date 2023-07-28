using Microsoft.Extensions.Configuration;

namespace GeekStore.Core.Helpers
{
    public static class ConfigurationExtensions
    {
        public static string? GetCaching(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("CacheSettings")[name];
        }
    }
}
