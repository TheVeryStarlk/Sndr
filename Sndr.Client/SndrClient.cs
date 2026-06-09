using System.Net.Http.Json;
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