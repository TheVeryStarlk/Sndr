using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Getting;

public sealed class TemplateResponse
{
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

    public required string Name { get; init; }

    public required string Subject { get; init; }

    public required string Html { get; init; }

    public string? Text { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; init; }
}