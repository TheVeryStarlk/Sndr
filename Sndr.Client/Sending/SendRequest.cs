using System.Text.Json.Serialization;

namespace Sndr.Client;

public sealed class SendRequest
{
    [JsonPropertyName("from")]
    public required string From { get; init; }

    [JsonPropertyName("to")]
    public required string To { get; init; }

    [JsonPropertyName("subject")]
    public required string Subject { get; init; }

    [JsonPropertyName("html")]
    public required string Html { get; init; }
}