namespace Lava.SDK.Client;

/// <summary>
/// Factory for creating Lava Business client instances with different API tokens.
/// Use this for multi-tenant scenarios where API tokens are retrieved dynamically.
/// </summary>
public interface ILavaBusinessClientFactory {
    /// <summary>
    /// Creates a Lava Business client instance with the specified API token.
    /// </summary>
    /// <param name="apiToken">Lava API token for this client instance.</param>
    /// <returns>A new LavaBusinessClient instance.</returns>
    ILavaBusinessClient CreateClient(string apiToken);
}
