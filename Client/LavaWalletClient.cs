using Lava.SDK.Infrastructure;
using Lava.SDK.Models.Requests;
using Lava.SDK.Models.Responses;
using System.Text.Json;

namespace Lava.SDK.Client;

/// <summary>
/// Client for Lava Wallet API operations.
/// </summary>
public class LavaWalletClient : ILavaWalletClient {
    private readonly LavaHttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="LavaWalletClient"/> class.
    /// </summary>
    /// <param name="httpClient">Configured HTTP client.</param>
    /// <param name="apiToken">Lava API token.</param>
    public LavaWalletClient(
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
    public async Task<InvoiceResponse> CreateInvoiceAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        return await httpClient.PostAsync<CreateInvoiceRequest, InvoiceResponse>(
            "invoice/create",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<InvoiceInfoResponse> GetInvoiceInfoAsync(
        string invoiceId,
        CancellationToken cancellationToken = default
    ) {
        if (string.IsNullOrWhiteSpace(invoiceId)) {
            throw new ArgumentException("Invoice ID cannot be empty.", nameof(invoiceId));
        }

        return await httpClient.PostAsync<InvoiceInfoResponse>(
            "invoice/info",
            invoiceId,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task SetWebhookUrlAsync(
        string webhookUrl,
        CancellationToken cancellationToken = default
    ) {
        if (string.IsNullOrWhiteSpace(webhookUrl)) {
            throw new ArgumentException("Webhook URL cannot be empty.", nameof(webhookUrl));
        }

        await httpClient.PostAsync<object>(
            "invoice/set-webhook",
            webhookUrl,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WalletInfo[]> GetWalletsAsync(
        CancellationToken cancellationToken = default
    ) {
        return await httpClient.GetAsync<WalletInfo[]>(
            "wallet/list",
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<StandardResponse> CreateWithdrawAsync(
        CreateWithdrawRequest request,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        return await httpClient.PostAsync<CreateWithdrawRequest, StandardResponse>(
            "withdraw/create",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<WithdrawInfo> GetWithdrawInfoAsync(
        string withdrawId,
        CancellationToken cancellationToken = default
    ) {
        if (string.IsNullOrWhiteSpace(withdrawId)) {
            throw new ArgumentException("Withdraw ID cannot be empty.", nameof(withdrawId));
        }

        return await httpClient.PostAsync<WithdrawInfo>(
            "withdraw/info",
            withdrawId,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<StandardResponse> CreateTransferAsync(
        CreateTransferRequest request,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            throw new ArgumentNullException(nameof(request));
        }

        return await httpClient.PostAsync<CreateTransferRequest, StandardResponse>(
            "transfer/create",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TransferInfo> GetTransferInfoAsync(
        string transferId,
        CancellationToken cancellationToken = default
    ) {
        if (string.IsNullOrWhiteSpace(transferId)) {
            throw new ArgumentException("Transfer ID cannot be empty.", nameof(transferId));
        }

        return await httpClient.PostAsync<TransferInfo>(
            "transfer/info",
            transferId,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TransactionInfo[]> GetTransactionsAsync(
        GetTransactionsRequest? request = null,
        CancellationToken cancellationToken = default
    ) {
        if (request == null) {
            return await httpClient.PostAsync<object, TransactionInfo[]>(
                "transactions/list",
                new { },
                cancellationToken
        ).ConfigureAwait(false);
        }

        return await httpClient.PostAsync<GetTransactionsRequest, TransactionInfo[]>(
            "transactions/list",
            request,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<SbpBanksResponse> GetSbpBanksAsync(
        CancellationToken cancellationToken = default
    ) {
        return await httpClient.PostAsync<object, SbpBanksResponse>(
            "withdraw/get-sbp-bank-list",
            new { },
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
