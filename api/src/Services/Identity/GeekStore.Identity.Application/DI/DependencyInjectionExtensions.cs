using GeekStore.Core.Results;
using GeekStore.Identity.Application.Tokens.Commands;
using GeekStore.Identity.Application.Tokens.Events;
using GeekStore.Identity.Application.Tokens.ViewModels;
using GeekStore.Identity.Application.Users.Commands;
using GeekStore.Identity.Application.Users.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Identity.Application.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddIdentityApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateRefreshTokenCommand, Result<Guid>>, TokenCommandHandler>();
            services.AddScoped<IRequestHandler<GenerateTokenCommand, Result<TokenResponseViewModel>>, TokenCommandHandler>();

            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<LoginCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserByEmailCommand, bool>, UserCommandHandler>();

            services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<RefreshTokenCreatedEvent>, RefreshTokenEventHandler>();

            return services;
        }
    }
}
