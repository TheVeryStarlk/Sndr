using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Sndr.Client.Webhooks.Receiving;

namespace Sndr.Client;

/// <summary>
/// Verifies and parses incoming SNDR webhook deliveries.
/// </summary>
/// <remarks>
/// SNDR signs every delivery as <c>t={timestamp},v1={hex(HMAC-SHA256(secret, "{timestamp}.{body}"))}</c> in the
/// <see cref="SignatureHeader"/> header. Always verify against the exact raw request body: re-serializing parsed
/// JSON changes key order and whitespace, which breaks the signature.
/// </remarks>
public static class SndrWebhook
{
    /// <summary>
    /// The name of the header carrying the signature of a webhook delivery.
    /// </summary>
    public const string SignatureHeader = "X-Sndr-Signature";

    /// <summary>
    /// The default maximum age of a delivery's timestamp: five minutes.
    /// </summary>
    public static readonly TimeSpan DefaultTolerance = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Verifies the signature of a webhook delivery.
    /// </summary>
    /// <param name="body">The raw request body, decoded as UTF-8.</param>
    /// <param name="signature">The value of the <see cref="SignatureHeader"/> header.</param>
    /// <param name="secret">The signing secret of the webhook endpoint.</param>
    /// <param name="tolerance">
    /// The maximum age of the delivery's timestamp, guarding against replayed deliveries. Defaults to
    /// <see cref="DefaultTolerance"/>; <see cref="TimeSpan.Zero"/> disables the freshness check.
    /// </param>
    /// <param name="timeProvider">The clock to check the timestamp against. Defaults to <see cref="TimeProvider.System"/>.</param>
    /// <returns><see langword="true"/> when the signature matches and the timestamp is fresh.</returns>
    public static bool Verify(string body, string? signature, string? secret, TimeSpan? tolerance = null, TimeProvider? timeProvider = null)
    {
        return Verify(Encoding.UTF8.GetBytes(body), signature, secret, tolerance, timeProvider);
    }

    /// <summary>
    /// Verifies the signature of a webhook delivery.
    /// </summary>
    /// <param name="body">The raw request body bytes.</param>
    /// <param name="signature">The value of the <see cref="SignatureHeader"/> header.</param>
    /// <param name="secret">The signing secret of the webhook endpoint.</param>
    /// <param name="tolerance">
    /// The maximum age of the delivery's timestamp, guarding against replayed deliveries. Defaults to
    /// <see cref="DefaultTolerance"/>; <see cref="TimeSpan.Zero"/> disables the freshness check.
    /// </param>
    /// <param name="timeProvider">The clock to check the timestamp against. Defaults to <see cref="TimeProvider.System"/>.</param>
    /// <returns><see langword="true"/> when the signature matches and the timestamp is fresh.</returns>
    public static bool Verify(ReadOnlySpan<byte> body, string? signature, string? secret, TimeSpan? tolerance = null, TimeProvider? timeProvider = null)
    {
        if (string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(secret))
        {
            return false;
        }

        string? timestamp = null;
        string? digest = null;

        foreach (var part in signature.Split(','))
        {
            var index = part.IndexOf('=');

            if (index < 0)
            {
                continue;
            }

            var name = part[..index].Trim();
            var value = part[(index + 1)..].Trim();

            if (name == "t")
            {
                timestamp = value;
            }
            else if (name == "v1")
            {
                digest = value;
            }
        }

        if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(digest))
        {
            return false;
        }

        var window = tolerance ?? DefaultTolerance;

        if (window > TimeSpan.Zero)
        {
            if (!double.TryParse(timestamp, NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds) || !double.IsFinite(seconds))
            {
                return false;
            }

            var now = (timeProvider ?? TimeProvider.System).GetUtcNow().ToUnixTimeSeconds();

            if (Math.Abs(now - seconds) > window.TotalSeconds)
            {
                return false;
            }
        }

        var prefix = Encoding.UTF8.GetBytes($"{timestamp}.");
        var payload = new byte[prefix.Length + body.Length];

        prefix.CopyTo(payload, 0);
        body.CopyTo(payload.AsSpan(prefix.Length));

        Span<byte> hash = stackalloc byte[HMACSHA256.HashSizeInBytes];
        HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), payload, hash);

        Span<char> expected = stackalloc char[HMACSHA256.HashSizeInBytes * 2];
        Convert.TryToHexStringLower(hash, expected, out _);

        // Length is not secret; the digest comparison itself must not leak how many characters matched.
        return digest.Length == expected.Length
               && CryptographicOperations.FixedTimeEquals(MemoryMarshal.AsBytes(expected), MemoryMarshal.AsBytes(digest.AsSpan()));
    }

    /// <summary>
    /// Parses the body of a webhook delivery. Verify the signature with <see cref="Verify(ReadOnlySpan{byte}, string?, string?, TimeSpan?, TimeProvider?)"/> first.
    /// </summary>
    /// <param name="body">The raw request body bytes.</param>
    /// <returns>
    /// An <see cref="EmailDeliveredEvent"/>, <see cref="EmailBouncedEvent"/> or <see cref="EmailUnsubscribedEvent"/>,
    /// or a plain <see cref="WebhookEventPayload"/> for an event type this library does not know.
    /// </returns>
    /// <exception cref="JsonException">The body is not a valid webhook payload.</exception>
    public static WebhookEventPayload? Deserialize(ReadOnlySpan<byte> body)
    {
        return JsonSerializer.Deserialize(body, SndrClientSerializationContext.Default.WebhookEventPayload);
    }

    /// <summary>
    /// Parses the body of a webhook delivery. Verify the signature with <see cref="Verify(string, string?, string?, TimeSpan?, TimeProvider?)"/> first.
    /// </summary>
    /// <param name="body">The raw request body.</param>
    /// <returns>
    /// An <see cref="EmailDeliveredEvent"/>, <see cref="EmailBouncedEvent"/> or <see cref="EmailUnsubscribedEvent"/>,
    /// or a plain <see cref="WebhookEventPayload"/> for an event type this library does not know.
    /// </returns>
    /// <exception cref="JsonException">The body is not a valid webhook payload.</exception>
    public static WebhookEventPayload? Deserialize(string body)
    {
        return JsonSerializer.Deserialize(body, SndrClientSerializationContext.Default.WebhookEventPayload);
    }
}