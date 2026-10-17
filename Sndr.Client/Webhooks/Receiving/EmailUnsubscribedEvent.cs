using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks.Receiving;

/// <summary>
/// Fired when a recipient follows the <c>List-Unsubscribe</c> link or their mail client posts the
/// one-click unsubscribe body (<c>email.unsubscribed</c>).
/// </summary>
public sealed class EmailUnsubscribedEvent : WebhookEventPayload
{
    /// <summary>
    /// The data of an unsubscribed event.
    /// </summary>
    public sealed class Unsubscription
    {
        /// <summary>
        /// The identifier of the email.
        /// </summary>
        [JsonPropertyName("email_id")]
        public required string Email { get; init; }

        /// <summary>
        /// Every recipient of the original email.
        /// </summary>
        public required string[] Addresses { get; init; }
    }

    /// <summary>
    /// The event's data.
    /// </summary>
    public required Unsubscription Data { get; init; }
}