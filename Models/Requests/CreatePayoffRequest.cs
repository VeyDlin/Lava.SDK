using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to create a payoff (Business API).
/// </summary>
public class CreatePayoffRequest {
    /// <summary>
    /// Payoff amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Unique payment identifier in merchant system.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Request signature.
    /// </summary>
    [JsonPropertyName("signature")]
    public string Signature { get; set; } = string.Empty;

    /// <summary>
    /// Project (shop) identifier.
    /// </summary>
    [JsonPropertyName("shopId")]
    public Guid ShopId { get; set; }

    /// <summary>
    /// Webhook URL for notifications (max 500 characters).
    /// </summary>
    [JsonPropertyName("hookUrl")]
    public string HookUrl { get; set; } = string.Empty;

    /// <summary>
    /// Service for payoff: "lava_payoff", "qiwi_payoff", "card_payoff", "steam_payoff".
    /// </summary>
    [JsonPropertyName("service")]
    public string Service { get; set; } = string.Empty;

    /// <summary>
    /// Recipient wallet number (omit for own Lava wallet).
    /// </summary>
    [JsonPropertyName("walletTo")]
    public string? WalletTo { get; set; }

    /// <summary>
    /// Who pays commission: "1" - merchant, "0" - deducted from amount.
    /// Default: "0".
    /// </summary>
    [JsonPropertyName("subtract")]
    public string Subtract { get; set; } = "0";
}
