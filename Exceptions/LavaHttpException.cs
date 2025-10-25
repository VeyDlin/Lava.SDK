namespace Lava.SDK.Exceptions;

/// <summary>
/// Exception thrown when HTTP request fails (network error, timeout, etc.).
/// </summary>
public class LavaHttpException : LavaApiException {
    /// <summary>
    /// Initializes a new instance of the <see cref="LavaHttpException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public LavaHttpException(string message, Exception innerException)
        : base(message, innerException) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaHttpException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaHttpException(string message, int statusCode, string? responseBody = null)
        : base(message, statusCode, responseBody) {
    }
}
