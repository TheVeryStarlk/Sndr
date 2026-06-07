using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed partial class SndrClient(IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory);

internal static class HttpClientExtensions
{
    extension(HttpClient client)
    {
        public async Task<TResponse?> GetAsync<TResponse>(
            string destination,
            JsonTypeInfo<TResponse> responseInfo)
        {
            var message = await client.GetAsync(destination);
        
            if (message.IsSuccessStatusCode)
            {
                // Serializer extensions for content.
                return await message.Content.ReadFromJsonAsync(responseInfo);
            }

            var failure = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SndrFailureResponse);

            ArgumentNullException.ThrowIfNull(failure);

            throw new SndrClientException(failure.Response.Code, failure.Response.Message, failure.Response.Request);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(
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
}