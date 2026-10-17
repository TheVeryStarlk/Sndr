using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Sending;

/// <summary>
/// The email accepted for sending.
/// </summary>
public sealed class SendEmailResponse
{
    /// <summary>
    /// The identifier of the email, usable with <see cref="SndrClient.GetEmailAsync"/>.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    /// <summary>
    /// The identifier of the owning organization.
    /// </summary>
    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

    /// <summary>
    /// The sender address.
    /// </summary>
    public required string From { get; init; }

    /// <summary>
    /// The recipient addresses.
    /// </summary>
    public required string[] To { get; init; }

    /// <summary>
    /// The rendered subject.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// The delivery status: <c>queued</c>, <c>sent</c>, <c>delivered</c>, <c>bounced</c>, <c>complained</c> or <c>failed</c>.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The identifier of the sending domain.
    /// </summary>
    [JsonPropertyName("domain_id")]
    public required string Domain { get; init; }

    /// <summary>
    /// The identifier of the template the email was rendered from, if any.
    /// </summary>
    [JsonPropertyName("template_id")]
    public string? Template { get; init; }

    /// <summary>
    /// The failure reason of a bounced or failed email.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// When the email was accepted for sending.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// When the email was delivered.
    /// </summary>
    public DateTimeOffset? DeliveredAt { get; init; }

    /// <summary>
    /// When the email was first opened.
    /// </summary>
    public DateTimeOffset? OpenedAt { get; init; }

    /// <summary>
    /// When a link in the email was first clicked.
    /// </summary>
    public DateTimeOffset? ClickedAt { get; init; }
}