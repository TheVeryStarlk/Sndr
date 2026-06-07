using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Sending;

public sealed class SendResponse
{
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

    [JsonPropertyName("from")]
    public required string From { get; init; }

    [JsonPropertyName("to")]
    public required string[] To { get; init; }

    [JsonPropertyName("subject")]
    public required string Subject { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("domain_id")]
    public required string Domain { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTimeOffset CreatedAt { get; init; }
}