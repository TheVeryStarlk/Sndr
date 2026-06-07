using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Sndr.Client;

public static class SndrClientServiceCollectionExtensions
{
    public static IServiceCollection AddSndrClient(this IServiceCollection services, Action<SndrClientOptions> configure)
    {
        // Probably should validate the options too?
        var options = new SndrClientOptions();

        configure(options);

        services
            .AddHttpClient(
                nameof(SndrClient),
                client =>
                {
                    // Think of a friendly way for API versioning. 
                    client.BaseAddress = new Uri("https://api.sndr.sh/v1/");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Key);
                })
            .AddResilienceHandler(
                nameof(SndrClient),
                builder => builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<HttpRequestException>()
                        .HandleResult(response => response.StatusCode is HttpStatusCode.TooManyRequests or >= HttpStatusCode.InternalServerError),

                    MaxRetryAttempts = options.MaximumAttempts,
                    Delay = options.BaseDelay,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    MaxDelay = options.MaximumDelay
                }));

        services.AddTransient<SndrClient>();

        return services;
    }
}