using System.Net.Http.Json;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<SendResponse?> SendAsync(SendRequest request)
    {
        const string destination = "https://api.sndr.sh/v1/send";

        var client = clientFactory.CreateClient(nameof(SndrClient));

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.Key}");

        var message = await client.PostAsJsonAsync(destination, request, SndrClientSerializationContext.Default.SendRequest);
        var response = await message.Content.ReadFromJsonAsync(SndrClientSerializationContext.Default.SendResponse);

        return response;
    }
}