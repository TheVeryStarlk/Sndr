using Sndr.Client.Templates.Getting;

namespace Sndr.Client;

public sealed partial class SndrClient
{
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