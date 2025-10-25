namespace Lava.SDK.Models.Enums;

/// <summary>
/// Payment and withdrawal service types supported by Lava.
/// </summary>
public enum ServiceType {
    /// <summary>Bank card transfer</summary>
    Card,

    /// <summary>QIWI wallet</summary>
    Qiwi,

    /// <summary>YooMoney wallet</summary>
    YooMoney,

    /// <summary>Lava wallet (internal transfer)</summary>
    Lava,

    /// <summary>AdvCash wallet</summary>
    AdvCash,

    /// <summary>Payeer wallet</summary>
    Payeer,

    /// <summary>Mobile phone number</summary>
    Phone,

    /// <summary>PerfectMoney wallet</summary>
    PerfectMoney,

    /// <summary>Fast Payment System (Faster Payments System)</summary>
    Sbp,

    /// <summary>Steam wallet</summary>
    Steam
}
