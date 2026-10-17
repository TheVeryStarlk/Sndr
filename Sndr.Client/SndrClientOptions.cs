namespace Sndr.Client;

/// <summary>
/// Options for the SNDR client and its underlying <see cref="HttpClient"/>.
/// </summary>
public sealed class SndrClientOptions
{
    /// <summary>
    /// The SNDR API key sent as a bearer token. Required.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// The versioned base address of the API. Must be an absolute <c>http</c> or <c>https</c> address.
    /// Defaults to <c>https://api.sndr.sh/v1/</c>.
    /// </summary>
    public Uri BaseAddress { get; set; } = new("https://api.sndr.sh/v1/");

    /// <summary>
    /// The overall time budget of a single call, including every retry attempt and the delays between them.
    /// Use <see cref="System.Threading.Timeout.InfiniteTimeSpan"/> to disable it. Defaults to 30 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// The total number of attempts, made for a call that fails with a network.
    /// </summary>
    public int MaximumAttempts { get; set; } = 3;

    /// <summary>
    /// The base of the exponential, jittered backoff between attempts. Defaults to 250 milliseconds.
    /// </summary>
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(250);

    /// <summary>
    /// The cap on a single delay between attempts, including delays requested by a <c>Retry-After</c> header.
    /// Defaults to 5 seconds.
    /// </summary>
    public TimeSpan MaximumDelay { get; set; } = TimeSpan.FromSeconds(5);
}