using System.Text.Json.Serialization;

namespace Lava.SDK.Models.Responses;

/// <summary>
/// SBP (Fast Payment System) bank information.
/// </summary>
public class SbpBank {
    /// <summary>
    /// Bank identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Bank name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Response containing list of SBP banks.
/// </summary>
public class SbpBanksResponse {
    /// <summary>
    /// List of available banks.
    /// </summary>
    [JsonPropertyName("data")]
    public List<SbpBank> Data { get; set; } = new();
}
