using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Lava.SDK.Infrastructure;

/// <summary>
/// Helper for generating HMAC-SHA256 signatures for Lava Business API.
/// </summary>
public static class SignatureHelper {
    /// <summary>
    /// Generates HMAC-SHA256 signature for the request body.
    /// </summary>
    /// <param name="jsonBody">JSON string of the request body.</param>
    /// <param name="secretKey">Secret key from Lava dashboard.</param>
    /// <returns>Hexadecimal signature string.</returns>
    public static string GenerateSignature(string jsonBody, string secretKey) {
        if (string.IsNullOrEmpty(jsonBody)) {
            throw new ArgumentException("JSON body cannot be empty.", nameof(jsonBody));
        }

        if (string.IsNullOrEmpty(secretKey)) {
            throw new ArgumentException("Secret key cannot be empty.", nameof(secretKey));
        }

        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var dataBytes = Encoding.UTF8.GetBytes(jsonBody);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(dataBytes);

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    /// <summary>
    /// Generates HMAC-SHA256 signature for the request object.
    /// </summary>
    /// <typeparam name="T">Request type.</typeparam>
    /// <param name="request">Request object to sign.</param>
    /// <param name="secretKey">Secret key from Lava dashboard.</param>
    /// <param name="options">JSON serializer options (optional).</param>
    /// <returns>Hexadecimal signature string.</returns>
    public static string GenerateSignature<T>(T request, string secretKey, JsonSerializerOptions? options = null) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        var jsonBody = JsonSerializer.Serialize(request, options ?? new JsonSerializerOptions());
        return GenerateSignature(jsonBody, secretKey);
    }

    /// <summary>
    /// Verifies webhook signature.
    /// </summary>
    /// <param name="jsonBody">JSON body received in webhook.</param>
    /// <param name="signature">Signature from webhook headers.</param>
    /// <param name="additionalKey">Additional key for webhook verification (from Lava dashboard).</param>
    /// <returns>True if signature is valid.</returns>
    public static bool VerifyWebhookSignature(string jsonBody, string signature, string additionalKey) {
        if (string.IsNullOrEmpty(jsonBody) || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(additionalKey)) {
            return false;
        }

        var expectedSignature = GenerateSignature(jsonBody, additionalKey);
        return string.Equals(expectedSignature, signature, StringComparison.OrdinalIgnoreCase);
    }
}
