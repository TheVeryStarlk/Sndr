namespace Sndr.Client.Analytics.Getting;

/// <summary>
/// Requests the analytics summary for a time range.
/// </summary>
public sealed class SummaryRequest
{
    /// <summary>
    /// The time range to summarize: <c>1d</c>, <c>7d</c>, <c>30d</c> or <c>90d</c>. Defaults to <c>30d</c>.
    /// </summary>
    public string Range { get; init; } = "30d";
}