namespace Sndr.Client;

public sealed partial class SndrClient
{
    public async Task<PagedEmailResponse?> GetAsync(int offset, int limit, string? cursor = null)
    {
        cursor = string.IsNullOrWhiteSpace(cursor) ? string.Empty : $"&cursor={cursor}";

        return await GetAsync(
            "/emails",
            $"?offset={offset}&limit={limit}{cursor}",
            SndrClientSerializationContext.Default.PagedEmailResponse);
    }

    public async Task<EmailResponse?> GetAsync(string identifier)
    {
        return await GetAsync(
            "/emails/",
            identifier,
            SndrClientSerializationContext.Default.EmailResponse);
    }
}