using MediatR;
using Polly;

namespace GeekStore.Website.Gateway.WebAPI.Behaviors
{
    public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly int _maxRetryAttempts;
        private readonly TimeSpan _pauseBetweenFailures;

        public RetryBehavior()
        {
            _maxRetryAttempts = 10;
            _pauseBetweenFailures = TimeSpan.FromMilliseconds(50);
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var retryPolicy = Policy
                                .Handle<Exception>(ex => ex is not OperationCanceledException)
                                .WaitAndRetryAsync(_maxRetryAttempts, attempt => _pauseBetweenFailures);

            return await retryPolicy.ExecuteAsync(async () => await next());
        }
    }
}