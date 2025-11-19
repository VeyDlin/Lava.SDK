namespace Lava.SDK.Exceptions;

/// <summary>
/// Exception thrown when requested resource is not found (404).
/// </summary>
public class LavaNotFoundException : LavaApiException {
    /// <summary>
    /// Initializes a new instance of the <see cref="LavaNotFoundException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public LavaNotFoundException(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaNotFoundException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="responseBody">Response body.</param>
    public LavaNotFoundException(string message, int statusCode, string? responseBody = null)
        : base(message, statusCode, responseBody) {
    }
}
