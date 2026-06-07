namespace Sndr.Client;

public sealed class SndrClientOptions
{
    public string? Key { get; set; }

    public int MaximumAttempts { get; set; } = 3;

    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(250);

    public TimeSpan MaximumDelay { get; set; } = TimeSpan.FromMilliseconds(500);
}