using System.Text.Json.Serialization;

namespace Sndr.Client.Domains.Getting;

public sealed class DomainResponse
{
    public sealed class Domain
    {
        public sealed class Record
        {
            public required string Type { get; init; }

            public required string Name { get; init; }

            public required string Value { get; init; }

            public required string Status { get; init; }

            public required string Hint { get; init; }

            public required bool Required { get; init; }

            public required string Label { get; init; }
        }

        [JsonPropertyName("id")]
        public required string Identifier { get; init; }

        [JsonPropertyName("organization_id")]
        public required string Organization { get; init; }

        public required string Name { get; init; }

        public required string Status { get; init; }

        public required string Region { get; init; }

        public required string SpfStatus { get; init; }

        public required string DkimStatus { get; init; }

        public required string DmarcStatus { get; init; }

        public required string MxStatus { get; init; }

        public required string TlsRptStatus { get; init; }

        public required string MtaStsStatus { get; init; }

        public required string BimiStatus { get; init; }

        public required Record[] Records { get; init; }

        public required DateTimeOffset CreatedAt { get; init; }
    }

    [JsonPropertyName("data")]
    public required Domain[] Domains { get; init; }
}