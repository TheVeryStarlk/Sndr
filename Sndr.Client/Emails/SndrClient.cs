using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Sndr.Client.Emails.Getting;
using Sndr.Client.Emails.Sending;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Lists sent emails, newest first.
    /// </summary>
    /// <param name="request">The page to fetch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A page of emails.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
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

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailPageResponse, cancellationToken);
    }

    /// <summary>
    /// Gets a single sent email.
    /// </summary>
    /// <param name="request">The email to get.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The email.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<EmailResponse?> GetEmailAsync(EmailRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync($"emails/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.EmailResponse, cancellationToken);
    }

    /// <summary>
    /// Sends a transactional email. The request carries an <c>Idempotency-Key</c> header that stays the same
    /// across retries, so a retried send is never delivered twice.
    /// </summary>
    /// <param name="request">The email to send.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The email accepted for sending.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<SendEmailResponse?> SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken = default)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, "send");

        message.Content = JsonContent.Create(request, SndrClientSerializationContext.Default.SendEmailRequest);
        message.Headers.Add("Idempotency-Key", request.IdempotencyKey ?? Guid.NewGuid().ToString());

        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.SendAsync(message, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.SendEmailResponse, cancellationToken);
    }
}