using System.Text.Json.Serialization;

namespace Sndr.Client;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]

// Emails.
[JsonSerializable(typeof(EmailResponse))]
[JsonSerializable(typeof(PagedEmailResponse))]

// Sending.
[JsonSerializable(typeof(SendRequest))]
[JsonSerializable(typeof(SendResponse))]

// Failure.
[JsonSerializable(typeof(SndrFailureResponse))]
[JsonSerializable(typeof(SndrFailureResponse.Failure))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;