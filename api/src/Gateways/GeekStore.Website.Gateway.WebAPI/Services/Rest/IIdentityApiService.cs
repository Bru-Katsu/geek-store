using GeekStore.Website.Gateway.WebAPI.Models.Identity;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface IIdentityApiService
    {
        [Post("/authentication/new-account")]
        Task<TokenResponse> CreateUser([Body] CreateUserRequest model);
        
        [Post("/authentication/authenticate")]
        Task<TokenResponse> Auth([Body] LoginRequest model);

        [Post("/authentication/refresh-token")]
        Task<TokenResponse> RefreshToken([Body] RefreshTokenRequest model);
    }
}
