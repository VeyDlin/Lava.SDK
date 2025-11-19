using Lava.SDK.Infrastructure;
using Lava.SDK.Models.Requests;
using Lava.SDK.Models.Responses;
using System.Text.Json;

namespace Lava.SDK.Client;

/// <summary>
/// Client for Lava Business API operations.
/// </summary>
public class LavaBusinessClient : ILavaBusinessClient {
    private readonly LavaHttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaBusinessClient"/> class.
    /// </summary>
    /// <param name="httpClient">Configured HTTP client.</param>
    /// <param name="apiToken">Lava API token.</param>
    public LavaBusinessClient(
        HttpClient httpClient,
        string apiToken
    ) {
        if (httpClient == null) {
            throw new ArgumentNullException(nameof(httpClient));
        }

        if (string.IsNullOrWhiteSpace(apiToken)) {
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));
        }

        this.httpClient = new LavaHttpClient(httpClient, apiToken);
    }

    /// <inheritdoc />
    public async Task<PayoffResponse> CreatePayoffAsync(
        CreatePayoffRequest request,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        return await httpClient.PostAsync<CreatePayoffRequest, PayoffResponse>(
            "business/payoff/create",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<PayoffResponse> GetPayoffInfoAsync(
        GetPayoffInfoRequest request,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        return await httpClient.PostAsync<GetPayoffInfoRequest, PayoffResponse>(
            "business/payoff/info",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> PingAsync(
        CancellationToken cancellationToken = default
    ) {
        try {
            var response = await httpClient.GetAsync<JsonDocument>(
                "test/ping",
                cancellationToken
        ).ConfigureAwait(false);

            return response.RootElement.TryGetProperty("status", out var statusElement) &&
                   statusElement.ValueKind == JsonValueKind.True;
        } catch {
            return false;
        }
    }
}
