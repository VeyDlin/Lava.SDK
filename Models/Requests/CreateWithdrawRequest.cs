using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to create a withdrawal.
/// </summary>
public class CreateWithdrawRequest {
    /// <summary>
    /// Your wallet number from which withdrawal is made.
    /// </summary>
    /// <example>R40510054</example>
    [JsonPropertyName("account")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Unique order identifier in your system (optional).
    /// </summary>
    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Webhook URL for notifications (optional).
    /// </summary>
    [JsonPropertyName("hook_url")]
    public string? HookUrl { get; set; }

    /// <summary>
    /// Who pays the commission: 0 - deducted from amount, 1 - paid from balance.
    /// Default: 0.
    /// </summary>
    [JsonPropertyName("subtract")]
    public int Subtract { get; set; } = 0;

    /// <summary>
    /// Withdrawal service type.
    /// </summary>
    /// <example>card</example>
    [JsonPropertyName("service")]
    public string Service { get; set; } = "card";

    /// <summary>
    /// Recipient's wallet/card number (optional for Lava wallet transfers).
    /// </summary>
    [JsonPropertyName("wallet_to")]
    public string? WalletTo { get; set; }

    /// <summary>
    /// Withdrawal comment (optional).
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Bank ID for SBP (Fast Payment System) transfers (optional).
    /// </summary>
    [JsonPropertyName("sbp_bank_id")]
    public string? SbpBankId { get; set; }
}
