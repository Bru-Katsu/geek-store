using GeekStore.Identity.Application.Tokens.Queries;
using GeekStore.Identity.Application.Tokens.ViewModels;
using GeekStore.Identity.Data.Context;
using GeekStore.Identity.Data.Tokens.Queries;
using GeekStore.Identity.Data.Tokens.Repositories;
using GeekStore.Identity.Domain.Token.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Identity.Data.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddIdentityDataServices(this IServiceCollection services)
        {
            services.AddTransient<DbContext, IdentityContext>(provider => provider.GetService<IdentityContext>());
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRequestHandler<RefreshTokenQuery, RefreshTokenViewModel>, RefreshTokenQueryHandler>();

            return services;
        }
    }
}
