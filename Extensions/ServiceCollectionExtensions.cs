using Lava.SDK.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Lava.SDK.Extensions;

/// <summary>
/// Extension methods for registering Lava API clients in dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions {
    private const string BaseApiUrl = "https://api.lava.ru/";

    /// <summary>
    /// Adds Lava Wallet API client to the service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="apiToken">Lava API token.</param>
    /// <returns>Service collection for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services or apiToken is null.</exception>
    /// <exception cref="ArgumentException">Thrown when apiToken is empty.</exception>
    public static IServiceCollection AddLavaWalletClient(
        this IServiceCollection services,
        string apiToken) {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (string.IsNullOrWhiteSpace(apiToken))
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));

        services.AddHttpClient<ILavaWalletClient, LavaWalletClient>((client) => {
            client.BaseAddress = new Uri(BaseApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddTypedClient((httpClient, sp) => new LavaWalletClient(httpClient, apiToken));

        return services;
    }

    /// <summary>
    /// Adds Lava Wallet API client to the service collection with custom configuration.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="apiToken">Lava API token.</param>
    /// <param name="configureClient">Action to configure HttpClient.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddLavaWalletClient(
        this IServiceCollection services,
        string apiToken,
        Action<HttpClient> configureClient) {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (string.IsNullOrWhiteSpace(apiToken))
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));
        if (configureClient == null) throw new ArgumentNullException(nameof(configureClient));

        services.AddHttpClient<ILavaWalletClient, LavaWalletClient>((client) => {
            client.BaseAddress = new Uri(BaseApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
            configureClient(client);
        })
        .AddTypedClient((httpClient, sp) => new LavaWalletClient(httpClient, apiToken));

        return services;
    }

    /// <summary>
    /// Adds Lava Business API client to the service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="apiToken">Lava API token.</param>
    /// <returns>Service collection for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services or apiToken is null.</exception>
    /// <exception cref="ArgumentException">Thrown when apiToken is empty.</exception>
    public static IServiceCollection AddLavaBusinessClient(
        this IServiceCollection services,
        string apiToken) {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (string.IsNullOrWhiteSpace(apiToken))
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));

        services.AddHttpClient<ILavaBusinessClient, LavaBusinessClient>((client) => {
            client.BaseAddress = new Uri(BaseApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddTypedClient((httpClient, sp) => new LavaBusinessClient(httpClient, apiToken));

        return services;
    }

    /// <summary>
    /// Adds Lava Business API client to the service collection with custom configuration.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="apiToken">Lava API token.</param>
    /// <param name="configureClient">Action to configure HttpClient.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddLavaBusinessClient(
        this IServiceCollection services,
        string apiToken,
        Action<HttpClient> configureClient) {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (string.IsNullOrWhiteSpace(apiToken))
            throw new ArgumentException("API token cannot be empty.", nameof(apiToken));
        if (configureClient == null) throw new ArgumentNullException(nameof(configureClient));

        services.AddHttpClient<ILavaBusinessClient, LavaBusinessClient>((client) => {
            client.BaseAddress = new Uri(BaseApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
            configureClient(client);
        })
        .AddTypedClient((httpClient, sp) => new LavaBusinessClient(httpClient, apiToken));

        return services;
    }
}
