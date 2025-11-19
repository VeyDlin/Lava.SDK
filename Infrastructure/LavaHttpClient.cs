using Lava.SDK.Exceptions;
using Lava.SDK.Models.Enums;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Lava.SDK.Infrastructure;

/// <summary>
/// HTTP client wrapper for Lava API communication.
/// </summary>
internal class LavaHttpClient {
    private readonly HttpClient httpClient;
    private readonly string apiToken;
    private static readonly JsonSerializerOptions JsonOptions = new() {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaHttpClient"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance.</param>
    /// <param name="apiToken">API authentication token.</param>
    public LavaHttpClient(HttpClient httpClient, string apiToken) {
        if (httpClient == null) {
            throw new ArgumentNullException(nameof(httpClient));
        }

        if (apiToken == null) {
            throw new ArgumentNullException(nameof(apiToken));
        }

        if (string.IsNullOrWhiteSpace(apiToken)) {
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));
        }

        this.httpClient = httpClient;
        this.apiToken = apiToken;
    }

    /// <summary>
    /// Sends GET request to the specified path.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="path">API endpoint path.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deserialized response.</returns>
    public async Task<TResponse> GetAsync<TResponse>(
        string path,
        CancellationToken cancellationToken = default) {
        var request = CreateRequest(HttpMethod.Get, path);
        return await SendRequestAsync<TResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends POST request with JSON body to the specified path.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="path">API endpoint path.</param>
    /// <param name="requestBody">Request body object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deserialized response.</returns>
    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string path,
        TRequest requestBody,
        CancellationToken cancellationToken = default) {
        var request = CreateRequest(HttpMethod.Post, path);

        var jsonContent = JsonSerializer.Serialize(requestBody, JsonOptions);
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        return await SendRequestAsync<TResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends POST request with string body to the specified path.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="path">API endpoint path.</param>
    /// <param name="body">Request body as string.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deserialized response.</returns>
    public async Task<TResponse> PostAsync<TResponse>(
        string path,
        string body,
        CancellationToken cancellationToken = default) {
        var request = CreateRequest(HttpMethod.Post, path);
        request.Content = new StringContent(body, Encoding.UTF8, "application/json");

        return await SendRequestAsync<TResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates HTTP request with authentication headers.
    /// </summary>
    private HttpRequestMessage CreateRequest(HttpMethod method, string path) {
        var request = new HttpRequestMessage(method, path);
        request.Headers.Add("Authorization", apiToken);
        request.Headers.Add("Accept", "application/json");
        return request;
    }

    /// <summary>
    /// Sends HTTP request and handles response.
    /// </summary>
    private async Task<TResponse> SendRequestAsync<TResponse>(
        HttpRequestMessage request,
        CancellationToken cancellationToken) {
        HttpResponseMessage response;

        try {
            response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        } catch (HttpRequestException ex) {
            throw new LavaHttpException("Failed to send HTTP request to Lava API.", ex);
        } catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested) {
            throw new LavaHttpException("Request to Lava API timed out.", ex);
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode) {
            HandleErrorResponse(response.StatusCode, responseBody);
        }

        try {
            var result = JsonSerializer.Deserialize<TResponse>(responseBody, JsonOptions);

            if (result == null) {
                throw new LavaApiException(
                    "Failed to deserialize API response: result is null.",
                    (int)response.StatusCode,
                    responseBody
                );
            }

            return result;
        } catch (JsonException ex) {
            throw new LavaApiException(
                $"Failed to deserialize API response: {ex.Message}",
                (int)response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Handles error responses from the API.
    /// </summary>
    private static void HandleErrorResponse(HttpStatusCode statusCode, string responseBody) {
        var statusCodeInt = (int)statusCode;

        // Authentication errors
        if (statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden) {
            throw new LavaAuthenticationException(
                "Authentication failed. Check your API token.",
                statusCodeInt,
                responseBody
            );
        }

        // Not found errors
        if (statusCode == HttpStatusCode.NotFound) {
            throw new LavaNotFoundException(
                "Resource not found.",
                statusCodeInt,
                responseBody
            );
        }

        // Try to parse error response
        try {
            using var doc = JsonDocument.Parse(responseBody);

            if (doc.RootElement.TryGetProperty("error_code", out var errorCodeElement) &&
                errorCodeElement.TryGetInt32(out var errorCodeValue)) {
                var errorCode = (ErrorCode)errorCodeValue;
                var errorMessage = doc.RootElement.TryGetProperty("message", out var msgElement)
                    ? msgElement.GetString() ?? "Unknown error"
                    : "Unknown error";

                // Validation errors
                if (IsValidationError(errorCode)) {
                    throw new LavaValidationException(
                        errorMessage,
                        errorCode,
                        statusCodeInt,
                        responseBody
                    );
                }

                throw new LavaApiException(
                    errorMessage,
                    errorCode,
                    statusCodeInt,
                    responseBody
                );
            }
        } catch (JsonException) {
            // If response is not JSON or doesn't contain expected fields, fall through
        }

        // Generic HTTP error
        throw new LavaHttpException(
            $"HTTP request failed with status code {statusCodeInt}.",
            statusCodeInt,
            responseBody
        );
    }

    /// <summary>
    /// Checks if error code is a validation error.
    /// </summary>
    private static bool IsValidationError(ErrorCode errorCode) {
        return errorCode switch {
            ErrorCode.InvalidParameterValue => true,
            ErrorCode.InvalidParameters => true,
            ErrorCode.InvalidInvoiceNumber => true,
            ErrorCode.AmountBelowMinimum => true,
            ErrorCode.AmountAboveMaximum => true,
            ErrorCode.ExpireBelowMinimum => true,
            ErrorCode.ExpireAboveMaximum => true,
            ErrorCode.OrderNumberTooLong => true,
            ErrorCode.OrderNumberAlreadyExists => true,
            _ => false
        };
    }
}
