namespace Sndr.Client.Emails.Getting;

/// <summary>
/// Requests a page of sent emails, newest first.
/// </summary>
public sealed class EmailPageRequest
{
    /// <summary>
    /// The number of emails to skip.
    /// </summary>
    public int? Offset { get; init; }

    /// <summary>
    /// The maximum number of emails to return.
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// The cursor of the page to fetch, taken from <see cref="EmailPageResponse.Next"/>.
    /// </summary>
    public string? Cursor { get; init; }
}