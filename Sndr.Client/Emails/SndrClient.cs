using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<EmailPageResponse?> GetEmailsAsync(EmailPageRequest request, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["offset"] = request.Offset?.ToString(),
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor
        };

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync(QueryHelpers.AddQueryString("emails", query), cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailPageResponse);
    }

    public async Task<EmailResponse?> GetEmailAsync(EmailRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync($"emails/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailResponse);
    }

    public async Task<SendEmailResponse?> SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("send", request, SndrClientSerializationContext.Default.SendEmailRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SendEmailResponse);
    }
}