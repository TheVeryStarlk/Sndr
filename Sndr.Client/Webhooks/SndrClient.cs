using System.Net.Http.Json;
using Sndr.Client.Webhooks.Creating;
using Sndr.Client.Webhooks.Deleting;
using Sndr.Client.Webhooks.Getting;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Registers a webhook endpoint.
    /// </summary>
    /// <param name="request">The endpoint and the events to deliver to it.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The registered webhook.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<WebhookResponse.Webhook?> CreateWebhookAsync(CreateWebhookRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("webhooks", request, SndrClientSerializationContext.Default.CreateWebhookRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.Webhook, cancellationToken);
    }

    /// <summary>
    /// Removes a webhook endpoint.
    /// </summary>
    /// <param name="request">The webhook to remove.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task DeleteWebhookAsync(DeleteWebhookRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.DeleteAsync($"webhooks/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        await response.ThrowIfFailedAsync(cancellationToken);
    }

    /// <summary>
    /// Lists the webhook endpoints of the organization.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The webhooks.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<WebhookResponse?> GetWebhooksAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("webhooks", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.WebhookResponse, cancellationToken);
    }
}