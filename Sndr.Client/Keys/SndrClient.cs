using Sndr.Client.Keys;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Lists the API keys of the organization.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The keys.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<KeyResponse?> GetKeysAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("api-keys", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.KeyResponse, cancellationToken);
    }
}