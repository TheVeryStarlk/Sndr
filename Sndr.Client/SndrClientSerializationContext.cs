using System.Text.Json.Serialization;
using Sndr.Client.Sending;

namespace Sndr.Client;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]

// Sending.
[JsonSerializable(typeof(SendRequest))]
[JsonSerializable(typeof(SendResponse))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;