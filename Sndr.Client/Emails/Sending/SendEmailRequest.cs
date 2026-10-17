using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Sending;

/// <summary>
/// Sends a transactional email, either from inline content or from a template.
/// </summary>
public sealed class SendEmailRequest
{
    /// <summary>
    /// A key/value tag attached to the email for filtering and analytics.
    /// </summary>
    public sealed class Tag
    {
        /// <summary>
        /// The tag's key.
        /// </summary>
        public required string Key { get; init; }

        /// <summary>
        /// The tag's value.
        /// </summary>
        public required string Value { get; init; }
    }

    /// <summary>
    /// The sender address, on a verified domain.
    /// </summary>
    public required string From { get; init; }

    /// <summary>
    /// The recipient addresses.
    /// </summary>
    public required string[] To { get; init; }

    /// <summary>
    /// The carbon copy recipient addresses.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Cc { get; init; }

    /// <summary>
    /// The blind carbon copy recipient addresses.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Bcc { get; init; }

    /// <summary>
    /// The address replies should go to.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReplyTo { get; init; }

    /// <summary>
    /// The subject.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// The HTML body. Optional when <see cref="Template"/> is set.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Html { get; init; }

    /// <summary>
    /// The plain text body.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; init; }

    /// <summary>
    /// The identifier of the template to render the email from.
    /// </summary>
    [JsonPropertyName("template_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; init; }

    /// <summary>
    /// Values for the <c>{{ name }}</c> placeholders in the template's subject, HTML and text.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string?>? Variables { get; init; }

    /// <summary>
    /// Tags attached to the email.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Tag[]? Tags { get; init; }

    /// <summary>
    /// Custom headers added to the email.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// The <c>Idempotency-Key</c> header of the send. When omitted a random key is generated per call.
    /// Either way the key is reused across retries, so a retried send is never delivered twice.
    /// </summary>
    [JsonIgnore]
    public string? IdempotencyKey { get; init; }
}