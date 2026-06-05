namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<SendResponse?> SendAsync(SendRequest request)
    {
        return await PostAsync(
            "/send",
            request,
            SndrClientSerializationContext.Default.SendRequest,
            SndrClientSerializationContext.Default.SendResponse);
    }
}