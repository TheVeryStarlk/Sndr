using System.Text.Json.Serialization;
using Sndr.Client.Analytics;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;

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
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;