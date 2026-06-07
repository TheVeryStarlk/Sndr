using System.Text.Json.Serialization;

namespace Sndr.Client;

public sealed class SndrClientException(string code, string message, string request) : Exception(message)
{
    public string Code => code;

    public string Request => request;
}

public sealed class SndrFailureResponse
{
    public sealed class Failure
    {
        [JsonPropertyName("code")]
        public required string Code { get; init; }

        [JsonPropertyName("message")]
        public required string Message { get; init; }

        [JsonPropertyName("request_id")]
        public required string Request { get; init; }
    }

    [JsonPropertyName("error")]
    public required Failure Response { get; init; }
}