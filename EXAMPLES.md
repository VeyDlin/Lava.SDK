# Lava.SDK Usage Examples

This file contains practical examples of using the Lava.SDK SDK.

## Table of Contents

- [ASP.NET Core Integration](#aspnet-core-integration)
- [Console Application](#console-application)
- [Creating Invoices](#creating-invoices)
- [Handling Webhooks](#handling-webhooks)
- [Withdrawals](#withdrawals)
- [Transfers](#transfers)
- [Error Handling](#error-handling)

## ASP.NET Core Integration

### Startup Configuration

```csharp
// Program.cs
using Lava.SDK.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Lava Wallet API client
builder.Services.AddLavaWalletClient(
    builder.Configuration["Lava:ApiToken"] ?? throw new Exception("Lava API token not configured")
);

// Optional: Custom configuration
builder.Services.AddLavaWalletClient("your-token", client => {
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
```

### appsettings.json

```json
{
  "Lava": {
    "ApiToken": "your-api-token-here"
  }
}
```

### Payment Controller

```csharp
using Lava.SDK.Client;
using Lava.SDK.Exceptions;
using Lava.SDK.Models.Requests;
using Lava.SDK.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase {
    private readonly ILavaWalletClient _lavaClient;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(
        ILavaWalletClient lavaClient,
        ILogger<PaymentController> logger) {
        _lavaClient = lavaClient;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto) {
        try {
            var request = new CreateInvoiceRequest {
                WalletTo = "R123456789",
                Amount = dto.Amount,
                OrderId = Guid.NewGuid().ToString(),
                Comment = dto.Description,
                HookUrl = Url.Action("Webhook", "Payment", null, Request.Scheme),
                SuccessUrl = dto.SuccessUrl,
                FailUrl = dto.FailUrl,
                CustomFields = $"userId:{dto.UserId}",
                MerchantName = "My Shop"
            };

            var invoice = await _lavaClient.CreateInvoiceAsync(request);

            return Ok(new {
                success = true,
                paymentUrl = invoice.Url,
                invoiceId = invoice.Id
            });
        } catch (LavaValidationException ex) {
            _logger.LogWarning(ex, "Validation error creating payment");
            return BadRequest(new { error = ex.Message });
        } catch (LavaApiException ex) {
            _logger.LogError(ex, "API error creating payment");
            return StatusCode(500, new { error = "Payment service error" });
        }
    }

    [HttpPost("webhook")]
    public IActionResult Webhook([FromBody] WebhookData webhook) {
        try {
            _logger.LogInformation(
                "Received webhook for invoice {InvoiceId}, status: {Status}",
                webhook.InvoiceId,
                webhook.Status);

            // TODO: Verify signature

            if (webhook.Status == "success") {
                // Extract user ID from custom fields
                var userId = ExtractUserId(webhook.CustomFields);

                // Update database, fulfill order, etc.
                ProcessSuccessfulPayment(webhook.OrderId, userId, webhook.Amount);
            }

            // CRITICAL: Always return 200 OK
            // Otherwise Lava will retry up to 15 times
            return Ok();
        } catch (Exception ex) {
            _logger.LogError(ex, "Error processing webhook");
            // Still return 200 to prevent retries
            return Ok();
        }
    }

    [HttpGet("status/{invoiceId}")]
    public async Task<IActionResult> GetStatus(string invoiceId) {
        try {
            var info = await _lavaClient.GetInvoiceInfoAsync(invoiceId);

            return Ok(new {
                invoiceId = info.Invoice.Id,
                status = info.Invoice.Status,
                amount = info.Invoice.Sum
            });
        } catch (LavaApiException ex) {
            _logger.LogError(ex, "Error fetching invoice status");
            return NotFound(new { error = "Invoice not found" });
        }
    }

    private string? ExtractUserId(string? customFields) {
        if (string.IsNullOrEmpty(customFields)) return null;
        var parts = customFields.Split(':');
        return parts.Length == 2 ? parts[1] : null;
    }

    private void ProcessSuccessfulPayment(string orderId, string? userId, string amount) {
        // Your business logic here
        _logger.LogInformation(
            "Processing payment: Order={OrderId}, User={UserId}, Amount={Amount}",
            orderId, userId, amount);
    }
}

public record CreatePaymentDto(
    decimal Amount,
    string Description,
    string UserId,
    string SuccessUrl,
    string FailUrl
);
```

## Console Application

```csharp
using Lava.SDK.Client;
using Lava.SDK.Exceptions;
using Lava.SDK.Models.Requests;

class Program {
    static async Task Main(string[] args) {
        var httpClient = new HttpClient {
            BaseAddress = new Uri("https://api.lava.ru/")
        };

        var lavaClient = new LavaWalletClient(httpClient, "YOUR_API_TOKEN");

        // Test connection
        Console.WriteLine("Testing API connection...");
        var isConnected = await lavaClient.PingAsync();

        if (!isConnected) {
            Console.WriteLine("Failed to connect to Lava API!");
            return;
        }

        Console.WriteLine("Connected successfully!");

        // Get wallets
        Console.WriteLine("\nFetching wallets...");
        var wallets = await lavaClient.GetWalletsAsync();

        foreach (var wallet in wallets) {
            Console.WriteLine($"  {wallet.Currency}: {wallet.Balance} ({wallet.Account})");
        }

        // Create invoice
        Console.WriteLine("\nCreating test invoice...");
        try {
            var invoice = await lavaClient.CreateInvoiceAsync(new CreateInvoiceRequest {
                WalletTo = wallets[0].Account,
                Amount = 10.00m,
                Comment = "Test payment from console app",
                Expire = 60 // 1 hour
            });

            Console.WriteLine($"Invoice created!");
            Console.WriteLine($"  ID: {invoice.Id}");
            Console.WriteLine($"  URL: {invoice.Url}");
            Console.WriteLine($"  Expires in: {invoice.Expire} minutes");
        } catch (LavaValidationException ex) {
            Console.WriteLine($"Validation error: {ex.Message}");
            Console.WriteLine($"Error code: {ex.ErrorCode}");
        } catch (LavaApiException ex) {
            Console.WriteLine($"API error: {ex.Message}");
        }
    }
}
```

## Creating Invoices

### Simple Invoice

```csharp
using Lava.SDK.Models.Requests;

var invoice = await lavaClient.CreateInvoiceAsync(new CreateInvoiceRequest {
    WalletTo = "R123456789",
    Amount = 100.00m
});
```

### Full-Featured Invoice

```csharp
using Lava.SDK.Models.Requests;
using System.Text.Json;

var invoice = await lavaClient.CreateInvoiceAsync(new CreateInvoiceRequest {
    WalletTo = "R123456789",
    Amount = 199.99m,
    OrderId = $"ORDER-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}",
    Comment = "Premium subscription - 1 month",
    Expire = 1440, // 24 hours
    HookUrl = "https://myapp.com/api/webhooks/lava",
    SuccessUrl = "https://myapp.com/payment/success",
    FailUrl = "https://myapp.com/payment/failed",
    CustomFields = JsonSerializer.Serialize(new {
        userId = 12345,
        planId = "premium_monthly",
        metadata = "any custom data"
    }),
    MerchantName = "My Application",
    Subtract = 1 // Customer pays commission
});

Console.WriteLine($"Payment URL: {invoice.Url}");
```

## Handling Webhooks

### Minimal Webhook Handler

```csharp
using Lava.SDK.Models.Responses;

[HttpPost("webhook")]
public IActionResult HandleWebhook([FromBody] WebhookData webhook) {
    if (webhook.Status == "success") {
        // Process payment
    }

    return Ok(); // ALWAYS return 200
}
```

### Complete Webhook Handler with Signature Verification

```csharp
using Lava.SDK.Models.Responses;
using System.Text;

[HttpPost("webhook")]
public IActionResult HandleWebhook([FromBody] WebhookData webhook) {
    // Verify signature
    var secretKey = _configuration["Lava:SecretKey"];
    var expectedSignature = ComputeSignature(webhook, secretKey);

    if (webhook.Sign != expectedSignature) {
        _logger.LogWarning("Invalid webhook signature");
        return Ok(); // Still return 200 to prevent retries
    }

    switch (webhook.Status) {
        case "success":
        HandleSuccessfulPayment(webhook);
        break;

        case "error":
        HandleFailedPayment(webhook);
        break;

        default:
        _logger.LogWarning("Unknown webhook status: {Status}", webhook.Status);
        break;
    }

    return Ok();
}

private string ComputeSignature(WebhookData webhook, string secretKey) {
    // Implement according to Lava API documentation
    // https://dev.lava.ru/
    var data = $"{webhook.InvoiceId}:{webhook.OrderId}:{webhook.Amount}:{secretKey}";
    using var sha256 = SHA256.Create();
    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
    return Convert.ToHexString(hash).ToLower();
}
```

## Withdrawals

### Card Withdrawal

```csharp
using Lava.SDK.Models.Requests;

var withdrawal = await lavaClient.CreateWithdrawAsync(new CreateWithdrawRequest {
    Account = "R123456789",
    Amount = 500.00m,
    Service = "card",
    WalletTo = "4111111111111111", // Card number
    Subtract = 1, // Pay commission from balance
    Comment = "Withdrawal to bank card"
});

Console.WriteLine($"Withdrawal ID: {withdrawal.Id}");
Console.WriteLine($"Status: {withdrawal.Status}");
```

### SBP Withdrawal

```csharp
// Get available banks
using Lava.SDK.Models.Requests;

var banksResponse = await lavaClient.GetSbpBanksAsync();
var sberbank = banksResponse.Data.First(b => b.Name.Contains("Сбербанк"));

// Create withdrawal
var withdrawal = await lavaClient.CreateWithdrawAsync(new CreateWithdrawRequest {
    Account = "R123456789",
    Amount = 1000.00m,
    Service = "sbp",
    WalletTo = "+79001234567",
    SbpBankId = sberbank.Id
});
```

## Transfers

```csharp
using Lava.SDK.Models.Requests;

var transfer = await lavaClient.CreateTransferAsync(new CreateTransferRequest {
    AccountFrom = "R111111111",
    AccountTo = "R222222222",
    Amount = 250.00m,
    Subtract = 0, // Deduct commission from amount
    Comment = "Transfer to partner account"
});

// Check transfer status
var transferInfo = await lavaClient.GetTransferInfoAsync(transfer.Id);
Console.WriteLine($"Transfer status: {transferInfo.Status}");
```

## Error Handling

### Comprehensive Error Handling

```csharp
using Lava.SDK.Exceptions;

try {
    var invoice = await lavaClient.CreateInvoiceAsync(request);
    // Success
} catch (LavaAuthenticationException ex) {
    // Invalid token, expired token, forbidden
    _logger.LogError(ex, "Authentication failed");
    return Unauthorized();
} catch (LavaValidationException ex) {
    // Invalid parameters, amount limits, etc.
    _logger.LogWarning(ex, "Validation failed: {ErrorCode}", ex.ErrorCode);

    return BadRequest(new {
        error = ex.Message,
        errorCode = ex.ErrorCode?.ToString()
    });
} catch (LavaHttpException ex) {
    // Network error, timeout, server error
    _logger.LogError(ex, "HTTP request failed: {StatusCode}", ex.StatusCode);
    return StatusCode(503, new { error = "Service temporarily unavailable" });
} catch (LavaApiException ex) {
    // Generic API error
    _logger.LogError(ex, "Lava API error");
    return StatusCode(500, new { error = "Payment service error" });
}
```

### Retry Logic

```csharp
using Lava.SDK.Exceptions;
using Polly;

var retryPolicy = Policy
    .Handle<LavaHttpException>()
    .WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var invoice = await retryPolicy.ExecuteAsync(async () =>
    await lavaClient.CreateInvoiceAsync(request));
```

## Transaction History

```csharp
// Get all transactions
using Lava.SDK.Models.Requests;

var allTransactions = await lavaClient.GetTransactionsAsync();

// Get filtered transactions
var withdrawals = await lavaClient.GetTransactionsAsync(new GetTransactionsRequest {
    TransferType = "withdraw",
    PeriodStart = "01.01.2024 00:00:00",
    PeriodEnd = "31.01.2024 23:59:59",
    Limit = 100
});

foreach (var transaction in withdrawals) {
    Console.WriteLine($"{transaction.CreatedAt}: {transaction.Amount} {transaction.Currency}");
}
```
