using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Analytics.Getting;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<SummaryResponse?> GetSummaryAsync(SummaryRequest request, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["range"] = request.Range
        };

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync(QueryHelpers.AddQueryString("analytics", query), cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SummaryResponse);
    }
}