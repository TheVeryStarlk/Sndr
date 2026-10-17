using System.Net.Http.Json;
using Sndr.Client.Domains.Creating;
using Sndr.Client.Domains.Deleting;
using Sndr.Client.Domains.Getting;
using Sndr.Client.Domains.Verification;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Adds a sending domain to the organization.
    /// </summary>
    /// <param name="request">The domain to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created domain, including the DNS records to publish.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<DomainResponse.Domain?> CreateDomainAsync(CreateDomainRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("domains", request, SndrClientSerializationContext.Default.CreateDomainRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.Domain, cancellationToken);
    }

    /// <summary>
    /// Removes a sending domain.
    /// </summary>
    /// <param name="request">The domain to remove.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task DeleteDomainAsync(DeleteDomainRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.DeleteAsync($"domains/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        await response.ThrowIfFailedAsync(cancellationToken);
    }

    /// <summary>
    /// Lists the sending domains of the organization.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The domains.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<DomainResponse?> GetDomainsAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("domains", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.DomainResponse, cancellationToken);
    }

    /// <summary>
    /// Re-checks the DNS records of a sending domain.
    /// </summary>
    /// <param name="request">The domain to verify.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The domain with its refreshed record statuses.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<DomainResponse.Domain?> VerifyDomainAsync(VerifyDomainRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsync($"domains/{Uri.EscapeDataString(request.Identifier)}/verify", null, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.Domain, cancellationToken);
    }
}