namespace Sndr.Client.Emails.Getting;

/// <summary>
/// Requests a single sent email.
/// </summary>
public sealed class EmailRequest
{
    /// <summary>
    /// The identifier of the email.
    /// </summary>
    public required string Identifier { get; init; }
}