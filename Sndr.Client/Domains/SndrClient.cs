using Sndr.Client.Domains;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<DomainResponse?> GetDomainsAsync()
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("domains");

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.DomainResponse);
    }

    public async Task<DomainResponse.Domain?> VerifyDomainAsync(VerifyDomainRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsync($"domains/{Uri.EscapeDataString(request.Identifier)}/verify", null);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.Domain);
    }
}