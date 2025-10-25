using Lava.SDK.Models.Requests;
using Lava.SDK.Models.Responses;

namespace Lava.SDK.Client;

/// <summary>
/// Interface for Lava Business API client.
/// </summary>
public interface ILavaBusinessClient {
    /// <summary>
    /// Creates a payoff (withdrawal for business accounts).
    /// </summary>
    /// <param name="request">Payoff creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Payoff creation response.</returns>
    Task<PayoffResponse> CreatePayoffAsync(
        CreatePayoffRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets information about a payoff.
    /// </summary>
    /// <param name="request">Payoff info request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Payoff information.</returns>
    Task<PayoffResponse> GetPayoffInfoAsync(
        GetPayoffInfoRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests API connection and token validity.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if connection is successful.</returns>
    Task<bool> PingAsync(
        CancellationToken cancellationToken = default);
}
