using GeekStore.Identity.Application.Tokens.Queries;
using GeekStore.Identity.Application.Tokens.ViewModels;
using GeekStore.Identity.Domain.Token;
using GeekStore.Identity.Domain.Token.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Identity.Data.Tokens.Queries
{
    internal class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenViewModel>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshTokenViewModel> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var query = _refreshTokenRepository.AsQueryable();
            var token = await query.FirstOrDefaultAsync(x => x.Id == request.Token, cancellationToken: cancellationToken);


            if (token == null)
                return default;
                
            return new RefreshTokenViewModel
            {
                Id = token.Id,
                ExpiresIn = token.ExpirationDate,
                UserName = token.UserName
            };
        }
    }
}
