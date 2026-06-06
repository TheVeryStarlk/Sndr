using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sndr.Client;

public sealed class SendResponse
{
    [JsonPropertyName("id")]
    public required string Identifier { get; init; }

    [JsonPropertyName("organization_id")]
    public required string Organization { get; init; }

    [JsonPropertyName("from")]
    public required string From { get; init; }

    [JsonPropertyName("to")]
    public required string[] To { get; init; }

    [JsonPropertyName("subject")]
    public required string Subject { get; init; }

    [JsonPropertyName("status")]
    public required Status Status { get; init; }

    [JsonPropertyName("domain_id")]
    public required string Domain { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTimeOffset CreatedAt { get; init; }
}

[JsonConverter(typeof(StatusConverter))]
public enum Status
{
    Queued,
    InProgress,
    Delivered,
    Bounced,
    Failed,
    Complained
}

public sealed class StatusConverter : JsonConverter<Status>
{
    public override Status Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return value switch
        {
            "queued" => Status.Queued,
            "in_progress" => Status.InProgress,
            "delivered" => Status.Delivered,
            "bounced" => Status.Bounced,
            "failed" => Status.Failed,
            "complained" => Status.Complained,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Write(Utf8JsonWriter writer, Status value, JsonSerializerOptions options)
    {
        var result = value switch
        {
            Status.Queued => "queued",
            Status.InProgress => "in_progress",
            Status.Delivered => "delivered",
            Status.Bounced => "bounced",
            Status.Failed => "failed",
            Status.Complained => "complained",
            _ => throw new ArgumentOutOfRangeException()
        };

        writer.WriteStringValue(result);
    }
}