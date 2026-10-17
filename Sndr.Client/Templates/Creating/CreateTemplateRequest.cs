using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Creating;

/// <summary>
/// Creates an email template. The subject, HTML and text may contain <c>{{ name }}</c> placeholders.
/// </summary>
public sealed class CreateTemplateRequest
{
    /// <summary>
    /// The display name of the template.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The subject.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// The HTML body.
    /// </summary>
    public required string Html { get; init; }

    /// <summary>
    /// The plain text body.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; init; }
}