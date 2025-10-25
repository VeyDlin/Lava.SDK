using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Requests;

/// <summary>
/// Request to get payoff information.
/// </summary>
public class GetPayoffInfoRequest {
    /// <summary>
    /// Request signature (optional if provided in headers).
    /// </summary>
    [JsonPropertyName("signature")]
    public string? Signature { get; set; }

    /// <summary>
    /// Shop (project) identifier.
    /// </summary>
    [JsonPropertyName("shopId")]
    public Guid ShopId { get; set; }

    /// <summary>
    /// Order identifier in merchant system (optional).
    /// </summary>
    [JsonPropertyName("orderId")]
    public Guid? OrderId { get; set; }

    /// <summary>
    /// Payoff identifier (optional).
    /// </summary>
    [JsonPropertyName("payoffId")]
    public Guid? PayoffId { get; set; }
}
