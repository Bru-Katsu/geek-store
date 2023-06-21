using MediatR;
using Polly;

public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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
            .Handle<Exception>()
            .WaitAndRetryAsync(_maxRetryAttempts, attempt => _pauseBetweenFailures);

        return await retryPolicy.ExecuteAsync(async () => await next());
    }
}
