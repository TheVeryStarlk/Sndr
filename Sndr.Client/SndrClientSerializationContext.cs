using System.Text.Json.Serialization;

namespace Sndr.Client;

[JsonSourceGenerationOptions(UseStringEnumConverter = true)]
[JsonSerializable(typeof(SendRequest))]
[JsonSerializable(typeof(SendResponse))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;