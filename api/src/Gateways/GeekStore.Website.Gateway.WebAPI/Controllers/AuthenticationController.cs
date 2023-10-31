using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.Models;
using GeekStore.Website.Gateway.WebAPI.Models.Identity;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : MainController
    {
        private readonly IIdentityApiService _identityApi;
        public AuthenticationController(INotificationService notificationService, IIdentityApiService authenticationService) : base(notificationService)
        {
            _identityApi = authenticationService;
        }

        [HttpPost("new-account")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequest createUser)
        {
            var result = await _identityApi.CreateUser(createUser);
            return Ok(result);
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _identityApi.Auth(request);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
        {
            var result = await _identityApi.RefreshToken(refreshToken);
            return Ok(result);
        }
    }
}
