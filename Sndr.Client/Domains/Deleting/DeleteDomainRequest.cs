namespace Sndr.Client.Domains.Deleting;

/// <summary>
/// Removes a sending domain.
/// </summary>
public sealed class DeleteDomainRequest
{
    /// <summary>
    /// The identifier of the domain.
    /// </summary>
    public required string Identifier { get; init; }
}