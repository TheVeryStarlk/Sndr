using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    private readonly IHttpClientFactory clientFactory;

    public SndrClient(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    public SndrClient(string key)
    {
        var collection = new ServiceCollection();

        collection.AddSndrClientHttpClient(options => options.Key = key);

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
