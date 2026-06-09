using Sndr.Client.Domains.Deleting;
using Sndr.Client.Domains.Getting;
using Sndr.Client.Domains.Verification;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task DeleteDomainAsync(DeleteDomainRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        await client.DeleteAsync($"domains/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);
    }
    
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