using GeekStore.Core.Extensions;

namespace GeekStore.Website.Gateway.WebAPI.Configurations
{
    public static class RedisConfig
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetCaching("RedisConnection");
            });

            return services;
        }
    }
}
