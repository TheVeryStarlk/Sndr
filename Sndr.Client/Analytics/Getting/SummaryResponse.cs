using System.Text.Json.Serialization;

namespace Sndr.Client.Analytics.Getting;

/// <summary>
/// Delivery statistics of the organization over a time range.
/// </summary>
public sealed class SummaryResponse
{
    /// <summary>
    /// A single bucket of the summary's time series.
    /// </summary>
    public sealed class Point
    {
        /// <summary>
        /// The start of the bucket.
        /// </summary>
        [JsonPropertyName("t")]
        public required DateTimeOffset Time { get; init; }

        /// <summary>
        /// The number of emails sent within the bucket.
        /// </summary>
        public required int Sent { get; init; }

        /// <summary>
        /// The number of emails delivered within the bucket.
        /// </summary>
        public required int Delivered { get; init; }

        /// <summary>
        /// The number of emails opened within the bucket.
        /// </summary>
        public required int Opened { get; init; }
    }

    /// <summary>
    /// The moment the summary was computed.
    /// </summary>
    [JsonPropertyName("as_of")]
    public required DateTimeOffset From { get; init; }

    /// <summary>
    /// The number of bounced emails.
    /// </summary>
    public required int Bounced { get; init; }

    /// <summary>
    /// The number of emails reported as spam.
    /// </summary>
    public required int Complained { get; init; }

    /// <summary>
    /// The number of delivered emails.
    /// </summary>
    public required int Delivered { get; init; }

    /// <summary>
    /// The number of emails that failed to send.
    /// </summary>
    public required int Failed { get; init; }

    /// <summary>
    /// The ratio of delivered emails that reached the inbox.
    /// </summary>
    public required double InboxRate { get; init; }

    /// <summary>
    /// The summarized time range, such as <c>30d</c>.
    /// </summary>
    public required string Range { get; init; }

    /// <summary>
    /// The number of sent emails.
    /// </summary>
    public required int Sent { get; init; }

    /// <summary>
    /// The number of opened emails.
    /// </summary>
    public int Opened { get; init; }

    /// <summary>
    /// The number of emails with at least one clicked link.
    /// </summary>
    public int Clicked { get; init; }

    /// <summary>
    /// The summary broken down over time.
    /// </summary>
    public Point[]? Series { get; init; }
}