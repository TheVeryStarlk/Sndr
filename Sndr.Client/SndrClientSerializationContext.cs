using System.Text.Json.Serialization;

namespace Sndr.Client;

[JsonSerializable(typeof(SendRequest))]
[JsonSerializable(typeof(SendResponse))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;