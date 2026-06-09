using System.Text.Json.Serialization;

namespace Sndr.Client.Analytics.Getting;

public sealed class SummaryResponse
{
    [JsonPropertyName("as_of")]
    public required DateTimeOffset From { get; init; }

    public required int Bounced { get; init; }

    public required int Complained { get; init; }
    
    public required int Delivered { get; init; }
    
    public required int Failed { get; init; }

    public required double InboxRate { get; init; }

    public required string Range { get; init; }
    
    public required int Sent { get; init; }
}