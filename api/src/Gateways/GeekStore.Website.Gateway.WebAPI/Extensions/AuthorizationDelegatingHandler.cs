using GeekStore.WebApi.Core.User;

namespace GeekStore.Website.Gateway.WebAPI.Extensions
{
    public class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IAspNetUser _aspNetUser;

        public AuthorizationDelegatingHandler(IAspNetUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authenticationHeader = _aspNetUser.GetHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authenticationHeader))
            {
                request.Headers.Add("Authorization", new string[] 
                { 
                    authenticationHeader 
                });
            }

            var token = _aspNetUser.GetUserToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
