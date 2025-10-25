namespace Lava.SDK.Models.Enums;

/// <summary>
/// Type of transaction.
/// </summary>
public enum TransactionType {
    /// <summary>Withdrawal from account</summary>
    Withdraw,

    /// <summary>Transfer between accounts</summary>
    Transfer,

    /// <summary>Incoming payment</summary>
    Income
}
