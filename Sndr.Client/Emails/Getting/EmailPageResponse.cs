using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Getting;

/// <summary>
/// A page of sent emails.
/// </summary>
public sealed class EmailPageResponse
{
    /// <summary>
    /// The emails of this page.
    /// </summary>
    [JsonPropertyName("data")]
    public required EmailResponse[] Emails { get; init; }

    /// <summary>
    /// The cursor of the next page, or <see langword="null"/> on the last page.
    /// </summary>
    [JsonPropertyName("next_cursor")]
    public required string? Next { get; init; }

    /// <summary>
    /// Whether more pages are available.
    /// </summary>
    [JsonPropertyName("has_more")]
    public required bool More { get; init; }

    /// <summary>
    /// The number of emails in this page.
    /// </summary>
    public required int Count { get; init; }

    /// <summary>
    /// The page size the server applied.
    /// </summary>
    public required int Limit { get; init; }
}