using Lava.SDK.Models.Requests;
using Lava.SDK.Models.Responses;

namespace Lava.SDK.Client;

/// <summary>
/// Interface for Lava Wallet API client.
/// </summary>
public interface ILavaWalletClient {
    /// <summary>
    /// Creates a payment invoice.
    /// </summary>
    /// <param name="request">Invoice creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created invoice information.</returns>
    Task<InvoiceResponse> CreateInvoiceAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets information about an invoice.
    /// </summary>
    /// <param name="invoiceId">Invoice identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Invoice information.</returns>
    Task<InvoiceInfoResponse> GetInvoiceInfoAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets webhook URL for receiving payment notifications.
    /// </summary>
    /// <param name="webhookUrl">HTTPS URL for webhook notifications.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SetWebhookUrlAsync(
        string webhookUrl,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets list of wallets on your account.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of wallet information.</returns>
    Task<WalletInfo[]> GetWalletsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a withdrawal request.
    /// </summary>
    /// <param name="request">Withdrawal request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Standard response with operation details.</returns>
    Task<StandardResponse> CreateWithdrawAsync(
        CreateWithdrawRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets information about a withdrawal.
    /// </summary>
    /// <param name="withdrawId">Withdrawal request identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Withdrawal information.</returns>
    Task<WithdrawInfo> GetWithdrawInfoAsync(
        string withdrawId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a transfer between Lava wallets.
    /// </summary>
    /// <param name="request">Transfer request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Standard response with operation details.</returns>
    Task<StandardResponse> CreateTransferAsync(
        CreateTransferRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets information about a transfer.
    /// </summary>
    /// <param name="transferId">Transfer identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Transfer information.</returns>
    Task<TransferInfo> GetTransferInfoAsync(
        string transferId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets list of transactions with optional filters.
    /// </summary>
    /// <param name="request">Transaction filter request (optional).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of transactions.</returns>
    Task<TransactionInfo[]> GetTransactionsAsync(
        GetTransactionsRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets list of banks connected to SBP (Fast Payment System).
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of SBP banks.</returns>
    Task<SbpBanksResponse> GetSbpBanksAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests API connection and token validity.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if connection is successful.</returns>
    Task<bool> PingAsync(
        CancellationToken cancellationToken = default);
}
