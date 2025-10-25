using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Detailed invoice information.
/// </summary>
public class InvoiceInfo {
    /// <summary>
    /// Invoice identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Order identifier from your system.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

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
    /// Invoice comment.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Invoice status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

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
}

/// <summary>
/// Response containing invoice information.
/// </summary>
public class InvoiceInfoResponse {
    /// <summary>
    /// Response status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Invoice details.
    /// </summary>
    [JsonPropertyName("invoice")]
    public InvoiceInfo Invoice { get; set; } = new();
}
