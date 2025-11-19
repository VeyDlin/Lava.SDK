namespace Lava.SDK.Client;

/// <summary>
/// Factory for creating Lava Business client instances with different API tokens.
/// Supports both ASP.NET Core DI with IHttpClientFactory and standalone usage.
/// </summary>
public class LavaBusinessClientFactory : ILavaBusinessClientFactory, IDisposable {
    private const string HttpClientName = "LavaBusiness";
    private const string BaseApiUrl = "https://api.lava.ru/";

    private readonly IHttpClientFactory? httpClientFactory;
    private readonly HttpClient? sharedHttpClient;
    private bool disposed = false;

    /// <summary>
    /// Initializes a new instance for use with ASP.NET Core dependency injection.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory from DI.</param>
    public LavaBusinessClientFactory(IHttpClientFactory httpClientFactory) {
        this.httpClientFactory = httpClientFactory
            ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// Initializes a new instance for standalone usage (console applications).
    /// Remember to dispose the factory when done.
    /// </summary>
    public LavaBusinessClientFactory() {
        sharedHttpClient = new HttpClient {
            BaseAddress = new Uri(BaseApiUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    /// <summary>
    /// Creates a Lava Business client instance with the specified API token.
    /// </summary>
    /// <param name="apiToken">Lava API token for this client instance.</param>
    /// <returns>A new LavaBusinessClient instance.</returns>
    /// <exception cref="ArgumentException">Thrown when apiToken is null or empty.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the factory has been disposed.</exception>
    public ILavaBusinessClient CreateClient(string apiToken) {
        if (disposed) {
            throw new ObjectDisposedException(nameof(LavaBusinessClientFactory));
        }

        if (string.IsNullOrWhiteSpace(apiToken)) {
            throw new ArgumentException(
                "API token cannot be empty.",
                nameof(apiToken)
            );
        }

        var httpClient = httpClientFactory?.CreateClient(HttpClientName)
            ?? sharedHttpClient
            ?? throw new InvalidOperationException("Factory not properly initialized.");

        return new LavaBusinessClient(httpClient, apiToken);
    }

    /// <summary>
    /// Disposes the shared HttpClient if using standalone mode.
    /// </summary>
    public void Dispose() {
        if (!disposed) {
            sharedHttpClient?.Dispose();
            disposed = true;
        }
    }

    /// <summary>
    /// Gets the HTTP client name used for DI registration.
    /// </summary>
    internal static string GetHttpClientName() => HttpClientName;

    /// <summary>
    /// Gets the base API URL.
    /// </summary>
    internal static string GetBaseApiUrl() => BaseApiUrl;
}
