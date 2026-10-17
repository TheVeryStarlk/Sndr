namespace Sndr.Client.Domains.Verification;

/// <summary>
/// Re-checks the DNS records of a sending domain.
/// </summary>
public sealed class VerifyDomainRequest
{
    /// <summary>
    /// The identifier of the domain.
    /// </summary>
    public required string Identifier { get; init; }
}