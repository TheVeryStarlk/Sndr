using System.Text.Json.Serialization;

namespace Sndr.Client.Domains.Creating;

/// <summary>
/// Adds a sending domain to the organization.
/// </summary>
public sealed class CreateDomainRequest
{
    /// <summary>
    /// The domain name, such as <c>mail.example.com</c>.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The region to host the domain in. The server picks its default when omitted.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DomainRegion? Region { get; init; }
}