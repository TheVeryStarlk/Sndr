namespace Sndr.Client.Webhooks.Deleting;

/// <summary>
/// Removes a webhook endpoint.
/// </summary>
public sealed class DeleteWebhookRequest
{
    /// <summary>
    /// The identifier of the webhook.
    /// </summary>
    public required string Identifier { get; init; }
}