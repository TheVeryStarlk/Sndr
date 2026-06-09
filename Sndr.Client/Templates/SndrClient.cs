using System.Net.Http.Json;
using Sndr.Client.Templates.Creating;
using Sndr.Client.Templates.Deleting;
using Sndr.Client.Templates.Getting;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<TemplateResponse?> CreateTemplateAsync(CreateTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("templates", request, SndrClientSerializationContext.Default.CreateTemplateRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplateResponse);
    }

    public async Task DeleteTemplateAsync(DeleteTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        await client.DeleteAsync($"templates/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);
    }

    public async Task<TemplatesResponse?> GetTemplatesAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("templates", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplatesResponse);
    }

    public async Task<TemplateResponse?> GetTemplateAsync(TemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync($"templates/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplateResponse);
    }
}