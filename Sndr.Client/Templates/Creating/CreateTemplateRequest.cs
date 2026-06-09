using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Creating;

public sealed class CreateTemplateRequest
{
    public required string Name { get; init; }

    public required string Subject { get; init; }

    public required string Html { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; init; }
}