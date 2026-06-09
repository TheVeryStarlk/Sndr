using System.Text.Json.Serialization;

namespace Sndr.Client.Templates.Getting;

public sealed class TemplatesResponse
{
    [JsonPropertyName("data")]
    public required TemplateResponse[] Templates { get; init; }
}