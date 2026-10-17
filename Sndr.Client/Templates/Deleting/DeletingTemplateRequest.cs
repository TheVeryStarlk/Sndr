namespace Sndr.Client.Templates.Deleting;

/// <summary>
/// Deletes an email template.
/// </summary>
public sealed class DeleteTemplateRequest
{
    /// <summary>
    /// The identifier of the template.
    /// </summary>
    public required string Identifier { get; init; }
}