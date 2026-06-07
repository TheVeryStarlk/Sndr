using Sndr.Client.Sending;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<SendResponse?> SendAsync(SendRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));

        return await client.PostAsync(
            "send",
            request,
            SndrClientSerializationContext.Default.SendRequest,
            SndrClientSerializationContext.Default.SendResponse);
    }
}