using System.Text.Json.Serialization;
using Sndr.Client.Analytics;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;
using Sndr.Client.Keys;

namespace Sndr.Client;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]

// Analytics.
[JsonSerializable(typeof(SummaryRequest))]
[JsonSerializable(typeof(SummaryResponse))]

// Failure.
[JsonSerializable(typeof(SndrFailureResponse))]
[JsonSerializable(typeof(SndrFailureResponse.Failure))]

// Emails.
[JsonSerializable(typeof(EmailResponse))]
[JsonSerializable(typeof(EmailPageResponse))]
[JsonSerializable(typeof(SendRequest))]
[JsonSerializable(typeof(SendResponse))]

// Keys.
[JsonSerializable(typeof(KeyResponse))]
[JsonSerializable(typeof(KeyResponse.Key))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;