using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GeekStore.WebApi.Core.User;
using GeekStore.Core.Extensions;
using Microsoft.Extensions.Configuration;
using GeekStore.WebApi.Core.Identity;

namespace GeekStore.Identity.Domain.Token.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IAspNetUser _aspNetUser;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;

        public UserTokenService(UserManager<Domain.User.User> userManager,
                                IJwtService jwtService,
                                IAspNetUser aspNetUser,
                                IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _aspNetUser = aspNetUser;
            _tokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;
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
            }).Concat(userRoles.Select(role => new Claim("role", role)));

            return new ClaimsIdentity(userClaims);
        }

        public async Task<string> EncodeJwtToken(ClaimsIdentity identityClaims)
        {
            var key = await _jwtService.GetCurrentSigningCredentials();

            //var appSettingsSection = _configuration.Get<AuthenticationSettings>();

            var issuer = new Uri("https://geekstore.identity.webapi:49170/jwks");
            var descriptor = new SecurityTokenDescriptor()
            {
                //Issuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}",
                Issuer = $"{issuer.Scheme}://{issuer.Authority}",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = key,
            };

            var token = _tokenHandler.CreateToken(descriptor);

            return _tokenHandler.WriteToken(token);
        }
    }
}
