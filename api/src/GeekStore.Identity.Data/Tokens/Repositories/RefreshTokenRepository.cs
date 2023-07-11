using GeekStore.Core.Data;
using GeekStore.Identity.Data.Context;
using GeekStore.Identity.Domain.Token.Repositories;

namespace GeekStore.Identity.Data.Tokens.Repositories
{
    internal class RefreshTokenRepository : Repository<IdentityContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityContext context) : base(context)
        { }
    }
}
