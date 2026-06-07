using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed class SndrClient(IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory)
{
}