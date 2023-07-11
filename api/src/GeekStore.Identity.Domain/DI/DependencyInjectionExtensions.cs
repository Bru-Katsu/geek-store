using GeekStore.Identity.Domain.Token.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Identity.Domain.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddIdentityDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserTokenService, UserTokenService>();

            return services;
        }
    }
}
