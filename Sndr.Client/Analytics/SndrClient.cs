using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Analytics.Getting;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Gets the delivery statistics of the organization over a time range.
    /// </summary>
    /// <param name="request">The time range to summarize.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The analytics summary.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<SummaryResponse?> GetSummaryAsync(SummaryRequest request, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["range"] = request.Range
        };

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync(QueryHelpers.AddQueryString("analytics", query), cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SummaryResponse, cancellationToken);
    }
}