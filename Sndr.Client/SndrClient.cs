using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Sndr.Client;

/// <summary>
/// Client for the SNDR API. Every resource (emails, domains, templates, API keys, webhooks and analytics)
/// is exposed as a set of asynchronous methods on this type.
/// </summary>
public sealed partial class SndrClient
{
    private readonly IHttpClientFactory clientFactory;

    /// <summary>
    /// Creates a client that resolves its <see cref="HttpClient"/> from the given factory.
    /// The factory must have the named client registered through
    /// <see cref="SndrClientServiceCollectionExtensions.AddSndrClientHttpClient"/>.
    /// </summary>
    /// <param name="clientFactory">The factory used to create the named SNDR <see cref="HttpClient"/>.</param>
    public SndrClient(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    /// <summary>
    /// Creates a standalone client authenticated with the given API key, using the default options.
    /// </summary>
    /// <param name="key">The SNDR API key.</param>
    public SndrClient(string key) : this(options => options.Key = key)
    {
    }

    /// <summary>
    /// Creates a standalone client configured by the given callback.
    /// </summary>
    /// <param name="configure">Configures the client options. <see cref="SndrClientOptions.Key"/> must be set.</param>
    public SndrClient(Action<SndrClientOptions> configure)
    {
        var collection = new ServiceCollection();

        collection.AddSndrClientHttpClient(configure);

        clientFactory = collection
            .BuildServiceProvider()
            .GetRequiredService<IHttpClientFactory>();
    }
}

internal static class ResponseMessageExtensions
{
    public static async Task<TResponse?> DeserializeOrThrowAsync<TResponse>(this HttpResponseMessage message, JsonTypeInfo<TResponse> typeInfo, CancellationToken cancellationToken = default)
    {
        await message.ThrowIfFailedAsync(cancellationToken);

        var content = await message.Content.ReadAsByteArrayAsync(cancellationToken);

        if (content.Length == 0)
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize(content, typeInfo);
        }
        catch (JsonException exception)
        {
            throw new SndrClientException("protocol_error", "Server returned a success status with an invalid JSON body.", null, message.StatusCode, exception);
        }
    }

    public static async Task ThrowIfFailedAsync(this HttpResponseMessage message, CancellationToken cancellationToken = default)
    {
        if (message.IsSuccessStatusCode)
        {
            return;
        }

        var status = (int) message.StatusCode;
        var content = await message.Content.ReadAsByteArrayAsync(cancellationToken);

        SndrFailureResponse? failure = null;

        if (content.Length > 0)
        {
            try
            {
                failure = JsonSerializer.Deserialize(content, SndrClientSerializationContext.Default.SndrFailureResponse);
            }
            catch (JsonException exception)
            {
                throw new SndrClientException(
                    "protocol_error",
                    $"Server returned a non-JSON response (HTTP {status}). The endpoint may not exist on this API version, or an upstream proxy returned the error.",
                    null,
                    message.StatusCode,
                    exception);
            }
        }

        throw new SndrClientException(
            failure?.Response?.Code ?? "unknown",
            failure?.Response?.Message ?? $"Request failed with status {status}.",
            failure?.Response?.Request,
            message.StatusCode);
    }
}
