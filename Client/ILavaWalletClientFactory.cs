namespace Lava.SDK.Client;

/// <summary>
/// Factory for creating Lava Wallet client instances with different API tokens.
/// Use this for multi-tenant scenarios where API tokens are retrieved dynamically.
/// </summary>
public interface ILavaWalletClientFactory {
    /// <summary>
    /// Creates a Lava Wallet client instance with the specified API token.
    /// </summary>
    /// <param name="apiToken">Lava API token for this client instance.</param>
    /// <returns>A new LavaWalletClient instance.</returns>
    ILavaWalletClient CreateClient(string apiToken);
}
