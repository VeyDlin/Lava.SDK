using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Wallet information.
/// </summary>
public class WalletInfo {
    /// <summary>
    /// Wallet account number.
    /// </summary>
    [JsonPropertyName("account")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// Wallet currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Current wallet balance.
    /// </summary>
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;
}
