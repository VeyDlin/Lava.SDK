using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to get transaction list with filters.
/// </summary>
public class GetTransactionsRequest {
    /// <summary>
    /// Transaction type filter: "withdraw" or "transfer" (optional).
    /// </summary>
    [JsonPropertyName("transfer_type")]
    public string? TransferType { get; set; }

    /// <summary>
    /// Wallet number filter (optional).
    /// </summary>
    [JsonPropertyName("account")]
    public string? Account { get; set; }

    /// <summary>
    /// Start date for transaction period (optional).
    /// </summary>
    /// <example>21.10.2021 10:30:30</example>
    [JsonPropertyName("period_start")]
    public string? PeriodStart { get; set; }

    /// <summary>
    /// End date for transaction period (optional).
    /// </summary>
    /// <example>21.10.2021 11:30:00</example>
    [JsonPropertyName("period_end")]
    public string? PeriodEnd { get; set; }

    /// <summary>
    /// Offset for pagination (optional).
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// Limit for number of results (optional).
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }
}
