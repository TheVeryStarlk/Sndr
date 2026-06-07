using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;

namespace Sndr.Client;

public sealed partial class SndrClient(IHttpClientFactory clientFactory);

internal static class ResponseMessageExtensions
{
    public static async Task<TResponse?> DeserializeOrThrowAsync<TResponse>(this HttpResponseMessage message, JsonTypeInfo<TResponse> typeInfo)
    {
        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync(typeInfo);
        }

        var failure = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SndrFailureResponse);

        ArgumentNullException.ThrowIfNull(failure);

        throw new SndrClientException(failure.Response.Code, failure.Response.Message, failure.Response.Request);
    }
}