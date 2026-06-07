using System.Text.Json.Serialization;

namespace Sndr.Client.Keys;

public sealed class KeyResponse
{
    public sealed class Key
    {
        [JsonPropertyName("id")]
        public required string Identifier { get; init; }

        [JsonPropertyName("organization_id")]
        public required string Organization { get; init; }

        public required string Name { get; init; }

        public required string Prefix { get; init; }

        public required string[] Scopes { get; init; }

        public required string LastUsedAt { get; init; }

        public required string CreatedAt { get; init; }
    }

    [JsonPropertyName("data")]
    public required Key[] Keys { get; init; }
}