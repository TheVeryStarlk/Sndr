namespace Sndr.Client.Emails.Sending;

public sealed class SendEmailRequest
{
    public required string From { get; init; }

    public required string[] To { get; init; }

    public required string Subject { get; init; }

    public required string Html { get; init; }
}