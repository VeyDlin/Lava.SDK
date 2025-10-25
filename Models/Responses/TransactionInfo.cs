using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Transaction information.
/// </summary>
public class TransactionInfo {
    /// <summary>
    /// Transaction identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Transaction creation timestamp.
    /// </summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>
    /// Transaction creation date and time.
    /// </summary>
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Transaction amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Transaction status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Transfer type: withdraw, transfer, etc.
    /// </summary>
    [JsonPropertyName("transfer_type")]
    public string TransferType { get; set; } = string.Empty;

    /// <summary>
    /// Transaction comment.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Payment method used.
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; set; }

    /// <summary>
    /// Currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Account number.
    /// </summary>
    [JsonPropertyName("account")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// Transaction commission.
    /// </summary>
    [JsonPropertyName("commission")]
    public string Commission { get; set; } = string.Empty;

    /// <summary>
    /// Transaction type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Receiver information.
    /// </summary>
    [JsonPropertyName("receiver")]
    public string? Receiver { get; set; }
}
