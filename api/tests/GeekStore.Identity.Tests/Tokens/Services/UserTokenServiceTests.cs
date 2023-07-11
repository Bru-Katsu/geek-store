using GeekStore.Identity.Domain.Token.Services;
using GeekStore.Identity.Tests.Fixtures;
using GeekStore.WebApi.Core.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.AutoMock;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GeekStore.Identity.Tests.Tokens.Services
{
    public class UserTokenServiceTests : IClassFixture<UserFixture>
    {
        private UserFixture _userFixture;

        public UserTokenServiceTests(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact(DisplayName = "Deve retornar claims")]
        [Trait("Services", "GetConfiguredUserClaims")]

        public async Task GetConfiguredUserClaims_ShouldReturnClaimsIdentityWithCorrectClaims()
        {
            // Arrange
            var user = _userFixture.CreateValidUser();
            var claims = new List<Claim>();

            var mock = new AutoMocker();

            mock.GetMock<UserManager<Domain.User.User>>()
                .Setup(manager => manager.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin", "User" });

            var service = mock.CreateInstance<UserTokenService>();
            
            // Act
            var result = await service.GetConfiguredUserClaims(claims, user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ClaimsIdentity>(result);

            var claimTypes = result.Claims.Select(c => c.Type).ToList();
            Assert.Contains(JwtRegisteredClaimNames.Sub, claimTypes);
            Assert.Contains(JwtRegisteredClaimNames.Email, claimTypes);
            Assert.Contains(JwtRegisteredClaimNames.Jti, claimTypes);
            Assert.Contains(JwtRegisteredClaimNames.Nbf, claimTypes);
            Assert.Contains(JwtRegisteredClaimNames.Iat, claimTypes);
            Assert.Contains("role", claimTypes);
        }

        [Fact]
        [Trait("Services", "EncodeJwtToken")]
        public async Task EncodeJwtToken_ShouldReturnEncodedJwtToken()
        {
            // Arrange
            var identityClaims = new ClaimsIdentity();
            var tokenHandlerMock = new Mock<JwtSecurityTokenHandler>();
            var tokenMock = new Mock<SecurityToken>();
            var signingCredentialsMock = new Mock<SigningCredentials>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextMock.Setup(x => x.Request.Scheme).Returns("http");
            httpContextMock.Setup(x => x.Request.Host).Returns(new HostString("localhost"));

            var securityKey = new byte[32]; // 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(securityKey);
            }

            var algorithm = SecurityAlgorithms.HmacSha256;

            var mock = new AutoMocker();

            mock.GetMock<IJwtService>()
                .Setup(s => s.GetCurrentSigningCredentials())
                .ReturnsAsync(new SigningCredentials(new SymmetricSecurityKey(securityKey), algorithm));
            
            mock.GetMock<JwtSecurityTokenHandler>()
                .Setup(x => x.CreateToken(It.IsAny<SecurityTokenDescriptor>()))
                .Returns(tokenMock.Object);

            mock.GetMock<JwtSecurityTokenHandler>()
                .Setup(x => x.WriteToken(tokenMock.Object))
                .Returns("encoded_jwt_token");

            mock.GetMock<IAspNetUser>()
                .Setup(x => x.GetHttpContext())
                .Returns(httpContextMock.Object);

            var service = mock.CreateInstance<UserTokenService>();

            // Act
            var result = await service.EncodeJwtToken(identityClaims);

            // Assert
            Assert.NotNull(result);
        }
    }
}
