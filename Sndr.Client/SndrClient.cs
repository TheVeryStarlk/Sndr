using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed partial class SndrClient(ILogger<SndrClient> logger, IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory)
{
    private const string Base = "https://api.sndr.sh/v1";

    private async Task<TResponse?> GetAsync<TResponse>(
        string destination,
        string query,
        JsonTypeInfo<TResponse> responseInfo)
    {
        logger.LogTrace("Creating get request to {Destination}", destination);

        var client = clientFactory.CreateClient(nameof(SndrClient));

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.Key}");

        var message = await client.GetAsync($"{Base}{destination}{query}");

        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync(responseInfo);
        }

        var failure = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SndrFailureResponse);

        ArgumentNullException.ThrowIfNull(failure);

        logger.LogTrace("Failure post to {Destination} with message {Message}", destination, failure.Response.Message);

        throw new SndrClientException(failure.Response.Code, failure.Response.Message, failure.Response.Request);
    }

    private async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string destination,
        TRequest request,
        JsonTypeInfo<TRequest> requestInfo,
        JsonTypeInfo<TResponse> responseInfo)
    {
        logger.LogTrace("Creating post request to {Destination}", destination);

        var client = clientFactory.CreateClient(nameof(SndrClient));

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.Key}");

        var message = await client.PostAsJsonAsync($"{Base}{destination}", request, requestInfo);

        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync(responseInfo);
        }

        var failure = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SndrFailureResponse);

        ArgumentNullException.ThrowIfNull(failure);

        logger.LogTrace("Failure post to {Destination} with message {Message}", destination, failure.Response.Message);

        throw new SndrClientException(failure.Response.Code, failure.Response.Message, failure.Response.Request);
    }
}