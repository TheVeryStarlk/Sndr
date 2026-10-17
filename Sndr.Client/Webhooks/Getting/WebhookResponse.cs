using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks.Getting;

/// <summary>
/// The webhook endpoints of the organization.
/// </summary>
public sealed class WebhookResponse
{
    /// <summary>
    /// A registered webhook endpoint.
    /// </summary>
    public sealed class Webhook
    {
        /// <summary>
        /// The identifier of the webhook.
        /// </summary>
        [JsonPropertyName("id")]
        public required string Identifier { get; init; }

        /// <summary>
        /// The identifier of the owning organization.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public required string Organization { get; init; }

        /// <summary>
        /// The URL SNDR posts the events to.
        /// </summary>
        public required string Url { get; init; }

        /// <summary>
        /// The subscribed events, such as <c>email.delivered</c>. See <see cref="WebhookEvent"/>.
        /// </summary>
        public required string[] Events { get; init; }

        /// <summary>
        /// The first characters of the signing secret, for recognizing it.
        /// </summary>
        public required string SecretPrefix { get; init; }

        /// <summary>
        /// Whether events are currently delivered to the endpoint.
        /// </summary>
        public required bool Active { get; init; }

        /// <summary>
        /// When the webhook was registered.
        /// </summary>
        public required DateTimeOffset CreatedAt { get; init; }
    }

    /// <summary>
    /// The webhooks.
    /// </summary>
    [JsonPropertyName("data")]
    public required Webhook[] Webhooks { get; init; }
}