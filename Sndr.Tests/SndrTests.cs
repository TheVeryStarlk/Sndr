using Sndr.Client;
using Sndr.Client.Emails.Getting;

namespace Sndr.Tests;

public sealed class SndrTests
{
    private readonly SndrClient client = new(Environment.GetEnvironmentVariable("Key") ?? throw new ArgumentException("No client key found."));

    [Fact]
    public async Task GettingSummaryAsync()
    {
        var request = new SummaryRequest();

        await client.GetSummaryAsync(request);
    }

    [Fact]
    public async Task GettingDomainsAsync()
    {
        await client.GetDomainsAsync();
    }

    [Fact]
    public async Task GettingEmailsAsync()
    {
        var request = new EmailPageRequest();

        await client.GetEmailsAsync(request);
    }
    
    [Fact]
    public async Task GettingKeysAsync()
    {
        await client.GetKeysAsync();
    }
}