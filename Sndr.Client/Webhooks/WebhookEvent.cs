using System.Text.Json.Serialization;

namespace Sndr.Client.Webhooks;

/// <summary>
/// An event a webhook endpoint can subscribe to.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<WebhookEvent>))]
public enum WebhookEvent
{
    /// <summary>
    /// The message was accepted by the receiving server (<c>email.delivered</c>).
    /// </summary>
    [JsonStringEnumMemberName("email.delivered")]
    EmailDelivered,

    /// <summary>
    /// The message could not be signed or was rejected (<c>email.bounced</c>).
    /// </summary>
    [JsonStringEnumMemberName("email.bounced")]
    EmailBounced,

    /// <summary>
    /// A recipient unsubscribed through the <c>List-Unsubscribe</c> link (<c>email.unsubscribed</c>).
    /// </summary>
    [JsonStringEnumMemberName("email.unsubscribed")]
    EmailUnsubscribed
}