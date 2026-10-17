using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks.Receiving;

/// <summary>
/// Fired when signing the message fails or the provider rejects the send (<c>email.bounced</c>).
/// </summary>
public sealed class EmailBouncedEvent : WebhookEventPayload
{
    /// <summary>
    /// The data of a bounced event.
    /// </summary>
    public sealed class Bounce
    {
        /// <summary>
        /// The identifier of the email.
        /// </summary>
        [JsonPropertyName("email_id")]
        public required string Email { get; init; }

        /// <summary>
        /// The reason of the bounce.
        /// </summary>
        public required string Error { get; init; }
    }

    /// <summary>
    /// The event's data.
    /// </summary>
    public required Bounce Data { get; init; }
}