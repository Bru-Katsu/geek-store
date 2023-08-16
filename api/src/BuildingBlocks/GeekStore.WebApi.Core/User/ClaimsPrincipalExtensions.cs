using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GeekStore.WebApi.Core.User
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(null, nameof(principal));            

            var claim = principal.FindFirst(JwtRegisteredClaimNames.Sub);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst(JwtRegisteredClaimNames.Email);
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
