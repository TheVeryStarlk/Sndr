using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Getting;

public sealed class EmailResponse
{
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

    public required string From { get; init; }

    public required string[] To { get; init; }

    public required string Subject { get; init; }

    public required string Status { get; init; }

    [JsonPropertyName("domain_id")]
    public required string Domain { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}