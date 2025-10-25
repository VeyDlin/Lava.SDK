using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Webhook notification data from Lava.
/// </summary>
public class WebhookData {
    /// <summary>
    /// Notification type.
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// Invoice identifier.
    /// </summary>
    [JsonPropertyName("invoice_id")]
    public string InvoiceId { get; set; } = string.Empty;

    /// <summary>
    /// Order identifier from your system.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Payment status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Payment timestamp.
    /// </summary>
    [JsonPropertyName("pay_time")]
    public int PayTime { get; set; }

    /// <summary>
    /// Payment amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Custom fields data.
    /// </summary>
    [JsonPropertyName("custom_fields")]
    public string? CustomFields { get; set; }

    /// <summary>
    /// Amount credited to balance.
    /// </summary>
    [JsonPropertyName("credited")]
    public string Credited { get; set; } = string.Empty;

    /// <summary>
    /// Merchant identifier.
    /// </summary>
    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = string.Empty;

    /// <summary>
    /// Merchant name.
    /// </summary>
    [JsonPropertyName("merchant_name")]
    public string MerchantName { get; set; } = string.Empty;

    /// <summary>
    /// Webhook signature for verification.
    /// </summary>
    [JsonPropertyName("sign")]
    public string Sign { get; set; } = string.Empty;
}
