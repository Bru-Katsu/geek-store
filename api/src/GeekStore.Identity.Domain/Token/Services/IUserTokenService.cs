using System.Security.Claims;

namespace GeekStore.Identity.Domain.Token.Services
{
    public interface IUserTokenService
    {
        Task<ClaimsIdentity> GetConfiguredUserClaims(IEnumerable<Claim> claims, Domain.User.User user);
        Task<string> EncodeJwtToken(ClaimsIdentity identityClaims);
    }
}
