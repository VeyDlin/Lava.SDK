namespace Lava.SDK.Models.Enums;

/// <summary>
/// Status of an invoice or payment.
/// </summary>
public enum InvoiceStatus {
    /// <summary>Invoice created, awaiting payment</summary>
    Created,

    /// <summary>Payment received and confirmed</summary>
    Success,

    /// <summary>Invoice expired without payment</summary>
    Expired,

    /// <summary>Payment cancelled or rejected</summary>
    Cancelled
}
