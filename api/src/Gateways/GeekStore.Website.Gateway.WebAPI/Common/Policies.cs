using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace GeekStore.Website.Gateway.WebAPI.Common
{
    public static class Policies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> TryWait()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                });

            return retry;
        }
    }
}
