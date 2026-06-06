using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed partial class SndrClient
{
    public SndrClient(IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory)
    {
    }

    public SndrClient(string key)
    {
    }
}