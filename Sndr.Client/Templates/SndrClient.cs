using System.Net.Http.Json;
using Sndr.Client.Templates.Creating;
using Sndr.Client.Templates.Deleting;
using Sndr.Client.Templates.Getting;
using Sndr.Client.Templates.Updating;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    /// <summary>
    /// Creates an email template.
    /// </summary>
    /// <param name="request">The template to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created template.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<TemplateResponse?> CreateTemplateAsync(CreateTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PostAsJsonAsync("templates", request, SndrClientSerializationContext.Default.CreateTemplateRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplateResponse, cancellationToken);
    }

    /// <summary>
    /// Deletes an email template.
    /// </summary>
    /// <param name="request">The template to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task DeleteTemplateAsync(DeleteTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.DeleteAsync($"templates/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        await response.ThrowIfFailedAsync(cancellationToken);
    }

    /// <summary>
    /// Lists the email templates of the organization.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The templates.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<TemplatesResponse?> GetTemplatesAsync(CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync("templates", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplatesResponse, cancellationToken);
    }

    /// <summary>
    /// Gets a single email template.
    /// </summary>
    /// <param name="request">The template to get.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The template.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<TemplateResponse?> GetTemplateAsync(TemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.GetAsync($"templates/{Uri.EscapeDataString(request.Identifier)}", cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplateResponse, cancellationToken);
    }

    /// <summary>
    /// Updates an email template. Only the properties set on the request are changed.
    /// </summary>
    /// <param name="request">The template and the changes to apply.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The updated template.</returns>
    /// <exception cref="SndrClientException">The API returned an error.</exception>
    public async Task<TemplateResponse?> UpdateTemplateAsync(UpdateTemplateRequest request, CancellationToken cancellationToken = default)
    {
        var client = clientFactory.CreateClient(nameof(SndrClient));
        var response = await client.PatchAsJsonAsync($"templates/{Uri.EscapeDataString(request.Identifier)}", request, SndrClientSerializationContext.Default.UpdateTemplateRequest, cancellationToken);

        return await response.DeserializeOrThrowAsync(SndrClientSerializationContext.Default.TemplateResponse, cancellationToken);
    }
}