using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GeekStore.Core.Extensions;
using Microsoft.Extensions.Configuration;
using GeekStore.WebApi.Core.User;

namespace GeekStore.Identity.Domain.Token.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly UserManager<User.User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private readonly IAspNetUser _aspNetUser;

        public UserTokenService(UserManager<User.User> userManager,
                                IJwtService jwtService,
                                IConfiguration configuration,
                                IAspNetUser aspNetUser)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _tokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;
            _aspNetUser = aspNetUser;
        }

        public async Task<ClaimsIdentity> GetConfiguredUserClaims(IEnumerable<Claim> claims, User.User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var userClaims = claims.Concat(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["AppTokenSettings:CartServiceAudience"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["AppTokenSettings:CustomerServiceAudience"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["AppTokenSettings:OrderServiceAudience"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["AppTokenSettings:ProductServiceAudience"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["AppTokenSettings:CouponServiceAudience"]),
            }).Concat(userRoles.Select(role => new Claim("role", role)));

            return new ClaimsIdentity(userClaims);
        }

        public async Task<string> EncodeJwtToken(ClaimsIdentity identityClaims)
        {
            var now = DateTime.Now;

            var key = await _jwtService.GetCurrentSigningCredentials();
            var issuer = new Uri(_configuration["AuthenticationSettings:JksUrlAuthentication"]);
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = $"{issuer.Scheme}://{issuer.Authority}",
                Subject = identityClaims,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddHours(_configuration.GetValue<int>("AppTokenSettings:TokenExpirationHours")),
                SigningCredentials = key,
            };

            var token = _tokenHandler.CreateToken(descriptor);

            return _tokenHandler.WriteToken(token);
        }
    }
}