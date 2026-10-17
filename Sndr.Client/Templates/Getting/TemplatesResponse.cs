using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Getting;

/// <summary>
/// The email templates of the organization.
/// </summary>
public sealed class TemplatesResponse
{
    /// <summary>
    /// The templates.
    /// </summary>
    [JsonPropertyName("data")]
    public required TemplateResponse[] Templates { get; init; }
}