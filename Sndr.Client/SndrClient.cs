using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sndr.Client;

public sealed partial class SndrClient(ILogger<SndrClient> logger, IOptions<SndrClientOptions> options, IHttpClientFactory clientFactory);