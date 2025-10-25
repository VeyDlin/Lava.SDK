using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// Payoff creation response data.
/// </summary>
public class PayoffData {
    /// <summary>
    /// Payoff identifier.
    /// </summary>
    [JsonPropertyName("payoff_id")]
    public string PayoffId { get; set; } = string.Empty;

    /// <summary>
    /// Payoff status: created, success, rejected.
    /// </summary>
    [JsonPropertyName("payoff_status")]
    public string PayoffStatus { get; set; } = string.Empty;
}

/// <summary>
/// Response for payoff creation.
/// </summary>
public class PayoffResponse {
    /// <summary>
    /// Payoff data.
    /// </summary>
    [JsonPropertyName("data")]
    public PayoffData Data { get; set; } = new();

    /// <summary>
    /// HTTP status code.
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Status check flag.
    /// </summary>
    [JsonPropertyName("status_check")]
    public bool StatusCheck { get; set; }
}
