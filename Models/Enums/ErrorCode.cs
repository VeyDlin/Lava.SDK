namespace Lava.SDK.Models.Enums;

/// <summary>
/// Error codes returned by the Lava API.
/// </summary>
public enum ErrorCode {
    /// <summary>Unknown error</summary>
    UnknownError = 0,

    /// <summary>Object not found</summary>
    ObjectNotFound = 1,

    /// <summary>Invalid parameter value</summary>
    InvalidParameterValue = 2,

    /// <summary>Invalid JWT token</summary>
    InvalidJwtToken = 5,

    /// <summary>Server error</summary>
    ServerError = 6,

    /// <summary>Invalid request type</summary>
    InvalidRequestType = 7,

    /// <summary>Invalid parameters provided</summary>
    InvalidParameters = 100,

    /// <summary>Invalid invoice number</summary>
    InvalidInvoiceNumber = 101,

    /// <summary>Amount is below minimum</summary>
    AmountBelowMinimum = 102,

    /// <summary>Amount exceeds maximum</summary>
    AmountAboveMaximum = 103,

    /// <summary>Insufficient balance</summary>
    InsufficientBalance = 104,

    /// <summary>Transaction not found</summary>
    TransactionNotFound = 105,

    /// <summary>Transfer unavailable</summary>
    TransferUnavailable = 107,

    /// <summary>Expire time is below minimum</summary>
    ExpireBelowMinimum = 202,

    /// <summary>Expire time exceeds maximum</summary>
    ExpireAboveMaximum = 203,

    /// <summary>Order number exceeds 255 characters</summary>
    OrderNumberTooLong = 204,

    /// <summary>Order number already exists</summary>
    OrderNumberAlreadyExists = 205,

    /// <summary>Invoice not found</summary>
    InvoiceNotFound = 206,

    /// <summary>Invoice has expired</summary>
    InvoiceExpired = 207,

    /// <summary>Invoice already paid</summary>
    InvoiceAlreadyPaid = 208,

    /// <summary>Secret key not set</summary>
    SecretKeyNotSet = 209,

    /// <summary>Invalid signature</summary>
    InvalidSignature = 210,

    /// <summary>Conversion unavailable</summary>
    ConversionUnavailable = 251
}
