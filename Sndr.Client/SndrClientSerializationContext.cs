using System.Text.Json.Serialization;
using Sndr.Client.Analytics.Getting;
using Sndr.Client.Domains.Creating;
using Sndr.Client.Domains.Getting;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;
using Sndr.Client.Keys;
using Sndr.Client.Templates.Getting;

namespace Sndr.Client;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]

// Analytics.
[JsonSerializable(typeof(SummaryRequest))]
[JsonSerializable(typeof(SummaryResponse))]

// Domains.
[JsonSerializable(typeof(CreateDomainRequest))]
[JsonSerializable(typeof(DomainResponse))]
[JsonSerializable(typeof(DomainResponse.Domain))]
[JsonSerializable(typeof(DomainResponse.Domain.Record))]

// Failure.
[JsonSerializable(typeof(SndrFailureResponse))]
[JsonSerializable(typeof(SndrFailureResponse.Failure))]

// Emails.
[JsonSerializable(typeof(EmailResponse))]
[JsonSerializable(typeof(EmailPageResponse))]
[JsonSerializable(typeof(SendEmailRequest))]
[JsonSerializable(typeof(SendEmailResponse))]

// Keys.
[JsonSerializable(typeof(KeyResponse))]
[JsonSerializable(typeof(KeyResponse.Key))]

// Templates.
[JsonSerializable(typeof(TemplatesResponse))]
[JsonSerializable(typeof(TemplateResponse))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;