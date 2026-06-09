using Sndr.Client.Domains;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<DomainResponse?> GetDomainsAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("domains", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.DomainResponse);
    }

    public async Task<DomainResponse.Domain?> VerifyDomainAsync(VerifyDomainRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsync($"domains/{Uri.EscapeDataString(request.Identifier)}/verify", null, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.Domain);
    }
}