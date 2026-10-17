using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks.Receiving;

/// <summary>
/// Fired after the receiving server accepts the message for at least one recipient (<c>email.delivered</c>).
/// </summary>
public sealed class EmailDeliveredEvent : WebhookEventPayload
{
    /// <summary>
    /// The data of a delivered event.
    /// </summary>
    public sealed class Delivery
    {
        /// <summary>
        /// The identifier of the email.
        /// </summary>
        [JsonPropertyName("email_id")]
        public required string Email { get; init; }

        /// <summary>
        /// The recipients the receiving server accepted.
        /// </summary>
        public required string[] To { get; init; }
    }

    /// <summary>
    /// The event's data.
    /// </summary>
    public required Delivery Data { get; init; }
}