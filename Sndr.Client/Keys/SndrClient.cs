using Sndr.Client.Keys;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<KeyResponse?> GetKeysAsync()
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("api-keys");

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.KeyResponse);
    }
}