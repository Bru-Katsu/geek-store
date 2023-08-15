using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GeekStore.Core.Helpers;
using GeekStore.WebApi.Core.User;

namespace GeekStore.Identity.Domain.Token.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IAspNetUser _aspNetUser;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UserTokenService(UserManager<Domain.User.User> userManager,
                                IJwtService jwtService,
                                IAspNetUser aspNetUser)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _aspNetUser = aspNetUser;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<ClaimsIdentity> GetConfiguredUserClaims(IEnumerable<Claim> claims, Domain.User.User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var userClaims = claims.Concat(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            }).Concat(userRoles.Select(role => new Claim("role", role)));

            return new ClaimsIdentity(userClaims);
        }

        public async Task<string> EncodeJwtToken(ClaimsIdentity identityClaims)
        {
            var key = await _jwtService.GetCurrentSigningCredentials();

            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = key,
            };

            var token = _tokenHandler.CreateToken(descriptor);

            return _tokenHandler.WriteToken(token);
        }
    }
}
