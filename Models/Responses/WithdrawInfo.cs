using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Withdrawal information.
/// </summary>
public class WithdrawInfo {
    /// <summary>
    /// Withdrawal request identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Creation timestamp (Unix timestamp format).
    /// </summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Commission amount.
    /// </summary>
    [JsonPropertyName("commission")]
    public string Commission { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Service type used for withdrawal.
    /// </summary>
    [JsonPropertyName("service")]
    public string Service { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal comment.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
}
