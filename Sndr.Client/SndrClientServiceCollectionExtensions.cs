using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Sndr.Client;

/// <summary>
/// Registers the SNDR client with an <see cref="IServiceCollection"/>.
/// </summary>
public static class SndrClientServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers <see cref="SndrClient"/> and its named <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="configure">Configures the client options. <see cref="SndrClientOptions.Key"/> must be set.</param>
        /// <returns>The same service collection, for chaining.</returns>
        public IServiceCollection AddSndrClient(Action<SndrClientOptions> configure)
        {
            services.AddSndrClientHttpClient(configure);
            services.AddTransient<SndrClient>();

            return services;
        }

        /// <summary>
        /// Registers only the named <see cref="HttpClient"/> used by <see cref="SndrClient"/>, with authentication,
        /// timeout and the retry policy applied.
        /// </summary>
        /// <param name="configure">Configures the client options. <see cref="SndrClientOptions.Key"/> must be set.</param>
        /// <returns>The same service collection, for chaining.</returns>
        /// <exception cref="ArgumentException">The configured options are invalid.</exception>
        public IServiceCollection AddSndrClientHttpClient(Action<SndrClientOptions> configure)
        {
            var options = new SndrClientOptions();

            configure(options);

            // Relative request paths are resolved against the base address, so it must end with a slash.
            var address = options.BaseAddress.AbsoluteUri.EndsWith('/') ? options.BaseAddress : new Uri($"{options.BaseAddress.AbsoluteUri}/");

            services
                .AddHttpClient(
                    nameof(SndrClient),
                    client =>
                    {
                        client.BaseAddress = address;
                        client.Timeout = options.Timeout;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Key);
                    })
                .AddResilienceHandler(
                    nameof(SndrClient),
                    builder => builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                    {
                        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                                .Handle<HttpRequestException>()
                                .HandleResult(response => response.StatusCode is HttpStatusCode.TooManyRequests or >= HttpStatusCode.InternalServerError),

                        Delay = options.BaseDelay,
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = true,
                        MaxDelay = options.MaximumDelay
                    }));

            return services;
        }
    }
}