namespace Sndr.Client.Templates.Getting;

/// <summary>
/// Requests a single email template.
/// </summary>
public sealed class TemplateRequest
{
    /// <summary>
    /// The identifier of the template.
    /// </summary>
    public required string Identifier { get; init; }
}