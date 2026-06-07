using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Analytics;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<SummaryResponse?> GetSummaryAsync(SummaryRequest request)
    {
        var query = new Dictionary<string, string?>
        {
            ["range"] = request.Range
        };

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync(QueryHelpers.AddQueryString("analytics", query));

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SummaryResponse);
    }
}