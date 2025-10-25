using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to create a transfer between Lava wallets.
/// </summary>
public class CreateTransferRequest {
    /// <summary>
    /// Source wallet number.
    /// </summary>
    [JsonPropertyName("account_from")]
    public string AccountFrom { get; set; } = string.Empty;

    /// <summary>
    /// Destination wallet number.
    /// </summary>
    [JsonPropertyName("account_to")]
    public string AccountTo { get; set; } = string.Empty;

    /// <summary>
    /// Transfer amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Who pays the commission: 0 - deducted from amount, 1 - paid from balance.
    /// Default: 0.
    /// </summary>
    [JsonPropertyName("subtract")]
    public int Subtract { get; set; } = 0;

    /// <summary>
    /// Transfer comment (optional).
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}
