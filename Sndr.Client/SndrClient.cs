using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed partial class SndrClient(IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory);

internal static class HttpClientExtensions
{
    public static async Task<TResponse?> PostAsync<TRequest, TResponse>(
        this HttpClient client,
        string destination,
        TRequest request,
        JsonTypeInfo<TRequest> requestInfo,
        JsonTypeInfo<TResponse> responseInfo)
    {
        var message = await client.PostAsJsonAsync(destination, request, requestInfo);

        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync(responseInfo);
        }

        var failure = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SndrFailureResponse);

        ArgumentNullException.ThrowIfNull(failure);

        throw new SndrClientException(failure.Response.Code, failure.Response.Message, failure.Response.Request);
    }
}