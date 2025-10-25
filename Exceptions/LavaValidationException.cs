using Lava.SDK.Models.Enums;

namespace Lava.SDK.Exceptions;

/// <summary>
/// Exception thrown when request validation fails (invalid parameters, amounts, etc.).
/// </summary>
public class LavaValidationException : LavaApiException {
    /// <summary>
    /// Initializes a new instance of the <see cref="LavaValidationException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public LavaValidationException(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaValidationException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="errorCode">API error code.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaValidationException(
        string message,
        ErrorCode errorCode,
        int statusCode,
        string? responseBody = null)
        : base(message, errorCode, statusCode, responseBody) {
    }
}
