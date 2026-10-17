namespace Sndr.Client.Webhooks.Creating;

/// <summary>
/// Registers a webhook endpoint.
/// </summary>
public sealed class CreateWebhookRequest
{
    /// <summary>
    /// The URL SNDR posts the events to.
    /// </summary>
    public required string Url { get; init; }

    /// <summary>
    /// The events to deliver to the endpoint.
    /// </summary>
    public required WebhookEvent[] Events { get; init; }
}