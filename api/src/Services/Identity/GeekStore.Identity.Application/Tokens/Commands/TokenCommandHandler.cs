using GeekStore.Core.Notifications;
using GeekStore.Identity.Application.Tokens.ViewModels;
using GeekStore.Identity.Domain.Token.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using GeekStore.Identity.Domain.Token.Services;
using GeekStore.Identity.Application.Tokens.Events;
using GeekStore.Identity.Domain.User;
using GeekStore.Identity.Domain.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using GeekStore.Core.Results;

namespace GeekStore.Identity.Application.Tokens.Commands
{
    public class TokenCommandHandler : IRequestHandler<GenerateTokenCommand, Result<TokenResponseViewModel>>,
                                       IRequestHandler<CreateRefreshTokenCommand, Result<Guid>>
    {
        private readonly INotificationService _notificationService;
        private readonly IUserTokenService _userTokenService;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly DbContext _context;

        public TokenCommandHandler(INotificationService notificationService,
                                   IUserTokenService userTokenService,
                                   IMediator mediator,
                                   UserManager<User> userManager,
                                   IRefreshTokenRepository refreshTokenRepository,
                                   IConfiguration configuration,
                                   DbContext context)
        {
            _notificationService = notificationService;
            _userTokenService = userTokenService;
            _mediator = mediator;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
            _context = context;
        }

        public async Task<Result<TokenResponseViewModel>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return new FailResult<TokenResponseViewModel>();
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var claims = await _userManager.GetClaimsAsync(user);
            var identityClaims = await _userTokenService.GetConfiguredUserClaims(claims, user);
            var encodedToken = await _userTokenService.EncodeJwtToken(identityClaims);
            var expirationHours = _configuration.GetValue<int>("AppTokenSettings:TokenExpirationHours");

            return new SuccessResult<TokenResponseViewModel>(new TokenResponseViewModel
            {
                AcessToken = encodedToken,
                RefreshToken = request.RefreshTokenId,
                ExpiresIn = TimeSpan.FromHours(expirationHours).TotalSeconds,
            });
        }

        public async Task<Result<Guid>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return default;
            }

            var hours = _configuration.GetValue<int>("AppTokenSettings:RefreshTokenExpirationHours");
            var expiresIn = DateTime.UtcNow.AddHours(hours);

            var refreshToken = new RefreshToken(request.Email, expiresIn);
            if (!refreshToken.IsValid())
            {
                _notificationService.AddNotifications(refreshToken.ValidationResult);
                return new FailResult<Guid>();
            }

            var refreshTokensByUser = await _refreshTokenRepository.Filter(x => x.UserName == request.Email);

            var transaction = _context.Database.BeginTransaction();

            foreach (var token in refreshTokensByUser)
            {
                _refreshTokenRepository.Delete(token);
            }

            _refreshTokenRepository.Insert(refreshToken);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await _mediator.Publish(new RefreshTokenCreatedEvent(refreshToken));

            return new SuccessResult<Guid>(refreshToken.Id);
        }
    }
}
