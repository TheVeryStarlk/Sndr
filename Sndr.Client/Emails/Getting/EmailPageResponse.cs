using System.Text.Json.Serialization;

namespace Sndr.Client.Emails.Getting;

public sealed class EmailPageResponse
{
    [JsonPropertyName("data")]
    public required EmailResponse[] Emails { get; set; }

    [JsonPropertyName("next_cursor")]
    public required string Next { get; set; }

    [JsonPropertyName("has_more")]
    public required bool More { get; set; }

    public required int Count { get; set; }

    public required int Limit { get; set; }
}