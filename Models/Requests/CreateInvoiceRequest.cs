using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to create a payment invoice.
/// </summary>
public class CreateInvoiceRequest {
    /// <summary>
    /// Your wallet number to receive payment.
    /// </summary>
    /// <example>R123456789</example>
    [JsonPropertyName("wallet_to")]
    public string WalletTo { get; set; } = string.Empty;

    /// <summary>
    /// Payment amount.
    /// </summary>
    /// <example>100.50</example>
    [JsonPropertyName("sum")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Unique order identifier in your system (optional).
    /// </summary>
    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Webhook URL to receive payment notifications (optional).
    /// </summary>
    [JsonPropertyName("hook_url")]
    public string? HookUrl { get; set; }

    /// <summary>
    /// URL to redirect after successful payment (optional).
    /// </summary>
    [JsonPropertyName("success_url")]
    public string? SuccessUrl { get; set; }

    /// <summary>
    /// URL to redirect after failed payment (optional).
    /// </summary>
    [JsonPropertyName("fail_url")]
    public string? FailUrl { get; set; }

    /// <summary>
    /// Invoice lifetime in minutes. Min: 1, Max: 43200 (30 days).
    /// Default: 43200.
    /// </summary>
    [JsonPropertyName("expire")]
    public int Expire { get; set; } = 43200;

    /// <summary>
    /// Who pays the commission: 0 - deducted from merchant, 1 - paid by customer.
    /// </summary>
    [JsonPropertyName("subtract")]
    public int? Subtract { get; set; }

    /// <summary>
    /// Custom data passed to webhook (optional).
    /// </summary>
    [JsonPropertyName("custom_fields")]
    public string? CustomFields { get; set; }

    /// <summary>
    /// Payment comment (optional).
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Merchant ID (returned in webhook only).
    /// </summary>
    [JsonPropertyName("merchant_id")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Merchant name displayed on payment form (optional).
    /// </summary>
    [JsonPropertyName("merchant_name")]
    public string? MerchantName { get; set; }
}
