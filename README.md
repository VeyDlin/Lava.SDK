# Lava.SDK

[![NuGet](https://img.shields.io/nuget/v/Lava.SDK.svg)](https://www.nuget.org/packages/Lava.SDK/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A modern, fully-featured .NET SDK for the [Lava.ru](https://lava.ru) payment gateway API.

## Features

- **Full API Coverage** - Complete support for both Wallet and Business APIs
- **Strongly Typed** - All requests and responses are strongly typed with XML documentation
- **Async/Await** - Modern async/await patterns with `ConfigureAwait(false)`
- **Dependency Injection** - Built-in support for `IHttpClientFactory` and ASP.NET Core DI
- **Error Handling** - Comprehensive exception hierarchy for different error scenarios
- **Production Ready** - Proper HttpClient management, cancellation token support

## Installation

```bash
dotnet add package Lava.SDK
```

## Quick Start

### For ASP.NET Core Applications (Recommended)

Register the Lava client in your `Program.cs`:

```csharp
using Lava.SDK.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Wallet API client
builder.Services.AddLavaWalletClient("YOUR_API_TOKEN");

// Or Business API client
builder.Services.AddLavaBusinessClient("YOUR_BUSINESS_API_TOKEN");

var app = builder.Build();
```

Then inject and use in your controllers:

```csharp
using Lava.SDK.Client;
using Lava.SDK.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase {
    private readonly ILavaWalletClient lavaClient;

    public PaymentController(ILavaWalletClient lavaClient) {
        lavaClient = lavaClient;
    }

    [HttpPost("create-invoice")]
    public async Task<IActionResult> CreateInvoice() {
        var request = new CreateInvoiceRequest {
            WalletTo = "R123456789",
            Amount = 100.00m,
            Comment = "Payment for order #12345",
            SuccessUrl = "https://yoursite.com/payment/success",
            FailUrl = "https://yoursite.com/payment/fail",
            HookUrl = "https://yoursite.com/api/payment/webhook"
        };

        var invoice = await lavaClient.CreateInvoiceAsync(request);
        return Ok(new { PaymentUrl = invoice.Url });
    }
}
```

### For Console Applications

```csharp
using Lava.SDK.Client;
using Lava.SDK.Models.Requests;

var httpClient = new HttpClient {
    BaseAddress = new Uri("https://api.lava.ru/")
};

var lavaClient = new LavaWalletClient(httpClient, "YOUR_API_TOKEN");

// Create an invoice
var invoice = await lavaClient.CreateInvoiceAsync(new CreateInvoiceRequest {
    WalletTo = "R123456789",
    Amount = 100.00m,
    Comment = "Test payment"
});

Console.WriteLine($"Payment URL: {invoice.Url}");
```

## API Reference

### Wallet API Client

The `ILavaWalletClient` provides access to personal wallet operations:

#### Invoice Operations

```csharp
// Create payment invoice
using Lava.SDK.Models.Requests;

var invoice = await client.CreateInvoiceAsync(new CreateInvoiceRequest {
    WalletTo = "R123456789",
    Amount = 100.00m,
    OrderId = "ORDER-12345",          // Optional: Your order ID
    Expire = 1440,                     // Optional: Lifetime in minutes (default: 43200)
    Comment = "Payment description",   // Optional
    HookUrl = "https://...",          // Optional: Webhook URL
    SuccessUrl = "https://...",       // Optional: Success redirect
    FailUrl = "https://...",          // Optional: Failure redirect
    CustomFields = "userId:12345",    // Optional: Custom data for webhook
    MerchantName = "My Shop"          // Optional: Display name
});

// Get invoice information
var info = await client.GetInvoiceInfoAsync("invoice-id");

// Set webhook URL
await client.SetWebhookUrlAsync("https://yoursite.com/webhook");
```

#### Wallet Operations

```csharp
// Get all wallets
var wallets = await client.GetWalletsAsync();
foreach (var wallet in wallets) {
    Console.WriteLine($"{wallet.Currency}: {wallet.Balance}");
}
```

#### Withdrawal Operations

```csharp
// Create withdrawal
using Lava.SDK.Models.Requests;

var withdraw = await client.CreateWithdrawAsync(new CreateWithdrawRequest {
    Account = "R123456789",          // Your wallet number
    Amount = 50.00m,
    Service = "card",                 // card, qiwi, yoomoney, etc.
    WalletTo = "4111111111111111",   // Recipient card/wallet
    Subtract = 0,                     // 0 = deduct from amount, 1 = from balance
    Comment = "Withdrawal"            // Optional
});

// Get withdrawal info
var withdrawInfo = await client.GetWithdrawInfoAsync(withdraw.Id);
```

#### Transfer Operations

```csharp
// Transfer between Lava wallets
using Lava.SDK.Models.Requests;

var transfer = await client.CreateTransferAsync(new CreateTransferRequest {
    AccountFrom = "R111111111",
    AccountTo = "R222222222",
    Amount = 25.00m,
    Subtract = 0,
    Comment = "Transfer"
});

// Get transfer info
var transferInfo = await client.GetTransferInfoAsync(transfer.Id);
```

#### Transaction History

```csharp
// Get all transactions
using Lava.SDK.Models.Requests;

var transactions = await client.GetTransactionsAsync();

// Get filtered transactions
var filtered = await client.GetTransactionsAsync(new GetTransactionsRequest {
    TransferType = "withdraw",
    Account = "R123456789",
    PeriodStart = "01.01.2024 00:00:00",
    PeriodEnd = "31.01.2024 23:59:59",
    Limit = 100,
    Offset = 0
});
```

#### Utility Methods

```csharp
// Get SBP (Fast Payment System) banks
var banks = await client.GetSbpBanksAsync();

// Test API connection
bool isConnected = await client.PingAsync();
```

### Business API Client

The `ILavaBusinessClient` provides access to business operations:

```csharp
// Create payoff
using Lava.SDK.Models.Requests;

var payoff = await businessClient.CreatePayoffAsync(new CreatePayoffRequest {
    Amount = 100.00m,
    OrderId = "ORDER-123",
    ShopId = Guid.Parse("your-shop-id"),
    Service = "card_payoff",
    Signature = "your-signature",
    HookUrl = "https://yoursite.com/webhook",
    WalletTo = "4111111111111111",
    Subtract = "0"
});

// Get payoff information
var payoffInfo = await businessClient.GetPayoffInfoAsync(new GetPayoffInfoRequest {
    ShopId = Guid.Parse("your-shop-id"),
    PayoffId = Guid.Parse("payoff-id"),
    Signature = "your-signature"
});
```

## Handling Webhooks

```csharp
using Lava.SDK.Models.Responses;

[HttpPost("webhook")]
public IActionResult HandleWebhook([FromBody] WebhookData webhook) {
    // Verify signature (implement your signature verification)

    if (webhook.Status == "success") {
        // Payment successful
        var orderId = webhook.OrderId;
        var amount = webhook.Amount;

        // Update your database, fulfill order, etc.
    }

    // IMPORTANT: Always return 200 OK
    // Otherwise, webhooks will be resent up to 15 times
    return Ok();
}
```

## Error Handling

The SDK provides a comprehensive exception hierarchy:

```csharp
using Lava.SDK.Exceptions;

try {
    var invoice = await client.CreateInvoiceAsync(request);
} catch (LavaAuthenticationException ex) {
    // Invalid API token, expired token
    Console.WriteLine($"Authentication failed: {ex.Message}");
} catch (LavaValidationException ex) {
    // Invalid parameters, amount too high/low, etc.
    Console.WriteLine($"Validation error: {ex.Message}");
    Console.WriteLine($"Error code: {ex.ErrorCode}");
} catch (LavaNotFoundException ex) {
    // Resource not found (404)
    Console.WriteLine($"Not found: {ex.Message}");
} catch (LavaHttpException ex) {
    // Network error, timeout, HTTP errors
    Console.WriteLine($"HTTP error: {ex.Message}");
    Console.WriteLine($"Status code: {ex.StatusCode}");
} catch (LavaApiException ex) {
    // Generic API error
    Console.WriteLine($"API error: {ex.Message}");
    Console.WriteLine($"Response: {ex.ResponseBody}");
}
```

## Signature Generation (Business API)

For Business API operations that require signatures:

```csharp
using Lava.SDK.Infrastructure;

// Generate signature for a request
var request = new CreatePayoffRequest {
    Amount = 100.00m,
    OrderId = "ORDER-123",
    ShopId = Guid.Parse("your-shop-id"),
    Service = "card_payoff",
    WalletTo = "4111111111111111"
};

// Generate signature using your secret key
var jsonBody = JsonSerializer.Serialize(request);
request.Signature = SignatureHelper.GenerateSignature(jsonBody, "your-secret-key");

// Or use the generic method
request.Signature = SignatureHelper.GenerateSignature(request, "your-secret-key");
```

### Webhook Signature Verification

```csharp
// Verify webhook signature
bool isValid = SignatureHelper.VerifyWebhookSignature(
    webhookJsonBody,
    signatureFromHeader,
    "your-additional-key"
);

if (!isValid) {
    return Unauthorized();
}
```

## Configuration Options

### Custom HttpClient Configuration

```csharp
builder.Services.AddLavaWalletClient("YOUR_TOKEN", client => {
    client.Timeout = TimeSpan.FromSeconds(60);
    client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
});
```

### Using with IHttpClientFactory

The SDK automatically uses `IHttpClientFactory` when registered via extension methods, ensuring proper HttpClient lifecycle management.

## Migration from v1.x

Version 2.0 is a complete rewrite with breaking changes:

### Key Changes

1. **API renamed**: `PublicLavaAPI` → `LavaWalletClient`, `BusinessLavaAPI` → `LavaBusinessClient`
2. **Interfaces**: All clients now have interfaces (`ILavaWalletClient`, `ILavaBusinessClient`)
3. **Dependency Injection**: Use `AddLavaWalletClient()` instead of manual instantiation
4. **All English**: Code, comments, and documentation are now in English
5. **Proper error handling**: Exceptions instead of returning `null`
6. **Request/Response models**: All in separate namespaces with clear naming

### Before (v1.x)

```csharp
var api = new PublicLavaAPI("token");
var payment = await api.CreatePaymentAsync(new() { ... });
if (payment == null) { /* error */ }
```

### After (v2.0)

```csharp
services.AddLavaWalletClient("token");

// Then inject ILavaWalletClient
var invoice = await client.CreateInvoiceAsync(new CreateInvoiceRequest { ... });
// Throws exceptions on error
```

## Documentation

- [Official Lava.ru API Documentation](https://dev.lava.ru/)
- [GitHub Repository](https://github.com/VeyDlin/Lava.SDK)

## License

MIT License - see [LICENSE](LICENSE) file for details.

## Support

For bugs and feature requests, please [open an issue](https://github.com/VeyDlin/Lava.SDK/issues) on GitHub.

---

Made with ❤️ for the .NET community
