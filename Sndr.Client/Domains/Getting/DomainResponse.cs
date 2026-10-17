using System.Text.Json.Serialization;

namespace Sndr.Client.Domains.Getting;

/// <summary>
/// The sending domains of the organization.
/// </summary>
public sealed class DomainResponse
{
    /// <summary>
    /// A sending domain and the DNS records it requires.
    /// </summary>
    public sealed class Domain
    {
        /// <summary>
        /// A DNS record to publish for the domain.
        /// </summary>
        public sealed class Record
        {
            /// <summary>
            /// The record type: <c>TXT</c>, <c>CNAME</c>, <c>MX</c> or <c>A</c>.
            /// </summary>
            public required string Type { get; init; }

            /// <summary>
            /// The record's host name.
            /// </summary>
            public required string Name { get; init; }

            /// <summary>
            /// The record's value.
            /// </summary>
            public required string Value { get; init; }

            /// <summary>
            /// The verification status of the record: <c>unknown</c>, <c>verified</c> or <c>failed</c>.
            /// </summary>
            public string? Status { get; init; }

            /// <summary>
            /// A hint on how to publish or fix the record.
            /// </summary>
            public string? Hint { get; init; }

            /// <summary>
            /// Whether the record is required for sending.
            /// </summary>
            public bool? Required { get; init; }

            /// <summary>
            /// A display label of the record, such as <c>DKIM</c>.
            /// </summary>
            public string? Label { get; init; }

            /// <summary>
            /// The priority of an <c>MX</c> record.
            /// </summary>
            public int? Priority { get; init; }

            /// <summary>
            /// The suggested time to live, in seconds.
            /// </summary>
            public int? Ttl { get; init; }
        }

        /// <summary>
        /// The identifier of the domain.
        /// </summary>
        [JsonPropertyName("id")]
        public required string Identifier { get; init; }

        /// <summary>
        /// The identifier of the owning organization.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public required string Organization { get; init; }

        /// <summary>
        /// The domain name.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The verification status of the domain: <c>pending</c>, <c>verified</c> or <c>failed</c>.
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// The region the domain is hosted in, such as <c>us-east-1</c>.
        /// </summary>
        public required string Region { get; init; }

        /// <summary>
        /// The SPF record status: <c>unknown</c>, <c>verified</c> or <c>failed</c>.
        /// </summary>
        public required string SpfStatus { get; init; }

        /// <summary>
        /// The DKIM record status: <c>unknown</c>, <c>verified</c> or <c>failed</c>.
        /// </summary>
        public required string DkimStatus { get; init; }

        /// <summary>
        /// The DMARC record status: <c>unknown</c>, <c>verified</c> or <c>failed</c>.
        /// </summary>
        public required string DmarcStatus { get; init; }

        /// <summary>
        /// The bounce MX record status. Absent when the record has not been published.
        /// </summary>
        public string? MxStatus { get; init; }

        /// <summary>
        /// The TLS-RPT record status. Absent when the record has not been published.
        /// </summary>
        public string? TlsRptStatus { get; init; }

        /// <summary>
        /// The MTA-STS record status. Absent when the record has not been published.
        /// </summary>
        public string? MtaStsStatus { get; init; }

        /// <summary>
        /// The BIMI record status. Absent when the record has not been published.
        /// </summary>
        public string? BimiStatus { get; init; }

        /// <summary>
        /// The DNS records to publish for the domain.
        /// </summary>
        public required Record[] Records { get; init; }

        /// <summary>
        /// When the domain was added.
        /// </summary>
        public required DateTimeOffset CreatedAt { get; init; }
    }

    /// <summary>
    /// The domains.
    /// </summary>
    [JsonPropertyName("data")]
    public required Domain[] Domains { get; init; }
}