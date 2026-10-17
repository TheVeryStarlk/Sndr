using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks.Receiving;

/// <summary>
/// The body of a webhook delivery.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type", IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(EmailDeliveredEvent), "email.delivered")]
[JsonDerivedType(typeof(EmailBouncedEvent), "email.bounced")]
[JsonDerivedType(typeof(EmailUnsubscribedEvent), "email.unsubscribed")]
public class WebhookEventPayload
{
    /// <summary>
    /// The identifier of the event delivery.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    /// <summary>
    /// When the event occurred.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}