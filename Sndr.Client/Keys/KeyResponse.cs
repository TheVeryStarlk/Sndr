using System.Text.Json.Serialization;

namespace Sndr.Client.Keys;

/// <summary>
/// The API keys of the organization. Keys can only be created and revoked from the dashboard.
/// </summary>
public sealed class KeyResponse
{
    /// <summary>
    /// An API key. The secret itself is never returned, only its prefix.
    /// </summary>
    public sealed class Key
    {
        /// <summary>
        /// The identifier of the key.
        /// </summary>
        [JsonPropertyName("id")]
        public required string Identifier { get; init; }

        /// <summary>
        /// The identifier of the owning organization.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public required string Organization { get; init; }

        /// <summary>
        /// The display name of the key.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The first characters of the key, for recognizing it.
        /// </summary>
        public required string Prefix { get; init; }

        /// <summary>
        /// The permissions of the key: <c>send</c> and/or <c>read</c>.
        /// </summary>
        public required string[] Scopes { get; init; }

        /// <summary>
        /// When the key was last used, or <see langword="null"/> if it never was.
        /// </summary>
        public DateTimeOffset? LastUsedAt { get; init; }

        /// <summary>
        /// When the key was created.
        /// </summary>
        public required DateTimeOffset CreatedAt { get; init; }
    }

    /// <summary>
    /// The keys.
    /// </summary>
    [JsonPropertyName("data")]
    public required Key[] Keys { get; init; }
}