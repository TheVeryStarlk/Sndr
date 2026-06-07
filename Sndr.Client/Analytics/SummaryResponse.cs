using System.Text.Json.Serialization;

namespace Sndr.Client.Analytics;

public sealed class SummaryResponse
{
    // Use JSON options instead of manually adding this attribute.
    [JsonPropertyName("as_of")]
    public required DateTimeOffset From { get; init; }

    [JsonPropertyName("bounced")]
    public required int Bounced { get; init; }

    [JsonPropertyName("complained")]
    public required int Complained { get; init; }
    
    [JsonPropertyName("delivered")]
    public required int Delivered { get; init; }
    
    [JsonPropertyName("failed")]
    public required int Failed { get; init; }

    [JsonPropertyName("inbox_rate")]
    public required double InboxRate { get; init; }

    [JsonPropertyName("range")]
    public required string Range { get; init; }
    
    [JsonPropertyName("sent")]
    public required int Sent { get; init; }
}