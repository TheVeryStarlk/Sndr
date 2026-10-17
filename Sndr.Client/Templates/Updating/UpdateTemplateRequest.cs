using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Updating;

/// <summary>
/// Updates an email template. Only the properties that are set are changed.
/// </summary>
public sealed class UpdateTemplateRequest
{
    /// <summary>
    /// The identifier of the template.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public required string Identifier { get; init; }

    /// <summary>
    /// The new display name, or <see langword="null"/> to keep the current one.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// The new subject, or <see langword="null"/> to keep the current one.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Subject { get; init; }

    /// <summary>
    /// The new HTML body, or <see langword="null"/> to keep the current one.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Html { get; init; }

    /// <summary>
    /// The new plain text body, or <see langword="null"/> to keep the current one.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; init; }
}