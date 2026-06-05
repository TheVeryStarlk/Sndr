namespace Sndr.Client;

public sealed class SndrClientException(string code, string message, string request) : Exception(message)
{
    public string Code => code;

    public string Request => request;
}