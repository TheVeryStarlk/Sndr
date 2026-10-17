using System.Net;
using System.Text.Json.Serialization;

namespace Sndr.Client;

/// <summary>
/// Thrown when the SNDR API returns an error response, or a response the client cannot understand.
/// </summary>
/// <param name="code">The machine-readable error code, such as <c>validation_error</c>, <c>protocol_error</c> or <c>unknown</c>.</param>
/// <param name="message">The human-readable error message.</param>
/// <param name="request">The SNDR request identifier, when the server supplied one.</param>
/// <param name="status">The HTTP status code of the response.</param>
/// <param name="innerException">The underlying exception, such as a JSON parsing failure.</param>
public sealed class SndrClientException(string code, string message, string? request, HttpStatusCode status = default, Exception? innerException = null) : Exception(message, innerException)
{
    /// <summary>
    /// The machine-readable error code returned by the API. The client uses <c>protocol_error</c> for unreadable
    /// responses and <c>unknown</c> for error responses that carry no SNDR error body.
    /// </summary>
    public string Code => code;

    /// <summary>
    /// The SNDR request identifier (<c>request_id</c>), useful when contacting support. Absent when the
    /// error did not originate from the SNDR API itself.
    /// </summary>
    public string? Request => request;

    /// <summary>
    /// The HTTP status code of the failed response.
    /// </summary>
    public HttpStatusCode Status => status;
}

/// <summary>
/// The error envelope returned by the SNDR API on every failed request.
/// </summary>
public sealed class SndrFailureResponse
{
    /// <summary>
    /// The error details of a failed request.
    /// </summary>
    public sealed class Failure
    {
        /// <summary>
        /// The machine-readable error code.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        /// <summary>
        /// The human-readable error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        /// <summary>
        /// The SNDR request identifier.
        /// </summary>
        [JsonPropertyName("request_id")]
        public string? Request { get; init; }
    }

    /// <summary>
    /// The error details.
    /// </summary>
    [JsonPropertyName("error")]
    public Failure? Response { get; init; }
}