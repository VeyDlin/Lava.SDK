namespace Lava.SDK.Exceptions;

/// <summary>
/// Exception thrown when authentication fails (invalid token, expired token, etc.).
/// </summary>
public class LavaAuthenticationException : LavaApiException {
    /// <summary>
    /// Initializes a new instance of the <see cref="LavaAuthenticationException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public LavaAuthenticationException(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaAuthenticationException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaAuthenticationException(string message, int statusCode, string? responseBody = null)
        : base(message, statusCode, responseBody) {
    }
}
