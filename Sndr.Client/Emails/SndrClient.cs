using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<EmailPageResponse?> GetEmailsAsync(EmailPageRequest request)
    {
        var query = new Dictionary<string, string?>
        {
            ["offset"] = request.Offset?.ToString(),
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor
        };

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync(QueryHelpers.AddQueryString("emails", query));

        // How about a context for each folder?
        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailPageResponse);
    }

    public async Task<EmailResponse?> GetEmailAsync(EmailRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync($"emails/{Uri.EscapeDataString(request.Identifier)}");

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailResponse);
    }

    public async Task<SendEmailResponse?> SendEmailAsync(SendEmailRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("send", request, SndrClientSerializationContext.Default.SendEmailRequest);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SendEmailResponse);
    }
}