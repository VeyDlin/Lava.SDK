using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Response containing created invoice information.
/// </summary>
public class InvoiceResponse {
    /// <summary>
    /// Request status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Invoice identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Payment URL for customer.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Invoice expiration time in minutes.
    /// </summary>
    [JsonPropertyName("expire")]
    public int Expire { get; set; }

    /// <summary>
    /// Invoice amount.
    /// </summary>
    [JsonPropertyName("sum")]
    public string Sum { get; set; } = string.Empty;

    /// <summary>
    /// Success redirect URL.
    /// </summary>
    [JsonPropertyName("success_url")]
    public string? SuccessUrl { get; set; }

    /// <summary>
    /// Failure redirect URL.
    /// </summary>
    [JsonPropertyName("fail_url")]
    public string? FailUrl { get; set; }

    /// <summary>
    /// Webhook URL.
    /// </summary>
    [JsonPropertyName("hook_url")]
    public string? HookUrl { get; set; }

    /// <summary>
    /// Custom fields data.
    /// </summary>
    [JsonPropertyName("custom_fields")]
    public string? CustomFields { get; set; }

    /// <summary>
    /// Merchant name.
    /// </summary>
    [JsonPropertyName("merchant_name")]
    public string? MerchantName { get; set; }

    /// <summary>
    /// Merchant identifier.
    /// </summary>
    [JsonPropertyName("merchant_id")]
    public string? MerchantId { get; set; }
}
