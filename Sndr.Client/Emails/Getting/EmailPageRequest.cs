namespace Sndr.Client.Emails.Getting;

public sealed class EmailPageRequest
{
    public int? Offset { get; init; }
    
    public int? Limit { get; init; }
    
    public string? Cursor { get; init; }
}