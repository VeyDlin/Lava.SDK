using Lava.SDK.Models.Enums;

namespace Lava.SDK.Exceptions;

/// <summary>
/// Base exception for all Lava API errors.
/// </summary>
public class LavaApiException : Exception {
    /// <summary>
    /// Error code returned by the API.
    /// </summary>
    public ErrorCode? ErrorCode { get; }

    /// <summary>
    /// HTTP status code of the response.
    /// </summary>
    public int? StatusCode { get; }

    /// <summary>
    /// Raw response body from the API.
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaApiException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public LavaApiException(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaApiException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public LavaApiException(string message, Exception innerException)
        : base(message, innerException) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaApiException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaApiException(string message, int statusCode, string? responseBody = null)
        : base(message) {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaApiException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="errorCode">API error code.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaApiException(
        string message,
        ErrorCode errorCode,
        int statusCode,
        string? responseBody = null)
        : base(message) {
        ErrorCode = errorCode;
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}
