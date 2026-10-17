using System.Text.Json.Serialization;
using Sndr.Client.Analytics.Getting;
using Sndr.Client.Domains.Creating;
using Sndr.Client.Domains.Getting;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;
using Sndr.Client.Keys;
using Sndr.Client.Templates.Creating;
using Sndr.Client.Templates.Getting;
using Sndr.Client.Templates.Updating;
using Sndr.Client.Webhooks.Creating;
using Sndr.Client.Webhooks.Getting;
using Sndr.Client.Webhooks.Receiving;

namespace Sndr.Client;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower, AllowOutOfOrderMetadataProperties = true)]

// Analytics.
[JsonSerializable(typeof(SummaryRequest))]
[JsonSerializable(typeof(SummaryResponse))]
[JsonSerializable(typeof(SummaryResponse.Point))]

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
[JsonSerializable(typeof(SendEmailRequest.Tag))]
[JsonSerializable(typeof(SendEmailResponse))]

// Keys.
[JsonSerializable(typeof(KeyResponse))]
[JsonSerializable(typeof(KeyResponse.Key))]

// Templates.
[JsonSerializable(typeof(CreateTemplateRequest))]
[JsonSerializable(typeof(UpdateTemplateRequest))]
[JsonSerializable(typeof(TemplatesResponse))]
[JsonSerializable(typeof(TemplateResponse))]

// Webhooks.
[JsonSerializable(typeof(CreateWebhookRequest))]
[JsonSerializable(typeof(WebhookResponse))]
[JsonSerializable(typeof(WebhookResponse.Webhook))]
[JsonSerializable(typeof(WebhookEventPayload))]
[JsonSerializable(typeof(EmailDeliveredEvent))]
[JsonSerializable(typeof(EmailBouncedEvent))]
[JsonSerializable(typeof(EmailUnsubscribedEvent))]
internal sealed partial class SndrClientSerializationContext : JsonSerializerContext;