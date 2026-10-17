using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Getting;

/// <summary>
/// An email template.
/// </summary>
public sealed class TemplateResponse
{
    /// <summary>
    /// The identifier of the template.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    /// <summary>
    /// The identifier of the owning organization.
    /// </summary>
    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

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
    public string? Text { get; init; }

    /// <summary>
    /// The revision of the template, incremented on every update.
    /// </summary>
    public int Version { get; init; }

    /// <summary>
    /// When the template was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// When the template was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}