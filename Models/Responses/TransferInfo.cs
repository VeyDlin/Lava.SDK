using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Transfer information.
/// </summary>
public class TransferInfo {
    /// <summary>
    /// Transfer identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Creation timestamp (Unix format).
    /// </summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>
    /// Transfer amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Transfer status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Transfer comment.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Transfer type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Receiver information.
    /// </summary>
    [JsonPropertyName("receiver")]
    public string Receiver { get; set; } = string.Empty;

    /// <summary>
    /// Commission amount.
    /// </summary>
    [JsonPropertyName("commission")]
    public string Commission { get; set; } = string.Empty;
}
