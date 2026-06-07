namespace Sndr.Client;

public sealed class SndrClientOptions
{
    public string? Key { get; set; }

    public int MaximumAttempts { get; init; } = 3;

    public TimeSpan BaseDelay { get; init; } = TimeSpan.FromMilliseconds(250);

    public TimeSpan MaximumDelay { get; init; } = TimeSpan.FromMilliseconds(500);
}