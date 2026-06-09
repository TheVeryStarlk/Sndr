using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Getting;

public sealed class EmailPageResponse
{
    [JsonPropertyName("data")]
    public required EmailResponse[] Emails { get; init; }

    [JsonPropertyName("next_cursor")]
    public required string Next { get; init; }

    [JsonPropertyName("has_more")]
    public required bool More { get; init; }

    public required int Count { get; init; }

    public required int Limit { get; init; }
}