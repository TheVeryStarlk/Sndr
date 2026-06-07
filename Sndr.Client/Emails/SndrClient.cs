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

        return await client.GetAsync(
            QueryHelpers.AddQueryString("emails", query),
            SndrClientSerializationContext.Default.EmailPageResponse);
    }

    public async Task<EmailResponse?> GetEmailAsync(EmailRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));

        return await client.GetAsync(
            $"emails/{Uri.EscapeDataString(request.Identifier)}",
            SndrClientSerializationContext.Default.EmailResponse);
    }

    public async Task<SendResponse?> SendEmailAsync(SendRequest request)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));

        return await client.PostAsync(
            "send",
            request,
            SndrClientSerializationContext.Default.SendRequest,
            SndrClientSerializationContext.Default.SendResponse);
    }
}