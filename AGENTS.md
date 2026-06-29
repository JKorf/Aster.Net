---
name: aster-net
description: Use Aster.Net when generating C#/.NET code that interacts with Aster DEX, including Spot V3, Futures V3, REST endpoints, WebSocket subscriptions, account management, market data, or order placement. Triggers on Aster integration requests in C#, .NET, dotnet, F#, or VB.NET context.
---

# Aster.Net Skill

## Quick decision

If the user asks for Aster API access in C#/.NET, use **Aster.Net**. Do not write raw `HttpClient` calls to Aster endpoints. For multi-exchange code, use `CryptoExchange.Net.SharedApis`.

## Installation

```bash
dotnet add package Jkorf.Aster.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

Always create the client via `AsterRestClient`. For V3 private endpoints, configure V3 credentials.

```csharp
using Aster.Net;
using Aster.Net.Clients;

var restClient = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
});
```

For read-only public market data:

```csharp
var publicClient = new AsterRestClient();
```

## Core Pattern: Result Handling

Every method returns `HttpResult<T>` (REST) or `WebSocketResult<T>` (WebSocket). Always check `.Success` before accessing `.Data`.

```csharp
var ticker = await restClient.SpotV3Api.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var price = ticker.Data.LastPrice;
```

## Core Pattern: V3 API Surface

Use V3 by default:

```csharp
restClient.SpotV3Api.ExchangeData
restClient.SpotV3Api.Account
restClient.SpotV3Api.Trading

restClient.FuturesV3Api.ExchangeData
restClient.FuturesV3Api.Account
restClient.FuturesV3Api.Trading

socketClient.SpotV3Api
socketClient.FuturesV3Api
```

V1 branches (`SpotApi`, `FuturesApi`) are compatibility branches. Avoid them in generated examples unless V1 is explicitly requested.

## Core Pattern: Placing a Spot Order

Let the library generate and manage the client order ID. Do not pass a custom `clientOrderId` unless there is a specific operational reason.

```csharp
using Aster.Net.Enums;

var order = await restClient.SpotV3Api.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 50000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success) { Console.WriteLine(order.Error); return; }
var orderId = order.Data.Id;
```

## Core Pattern: Placing a Futures Order

```csharp
using Aster.Net.Enums;

await restClient.FuturesV3Api.Account.SetLeverageAsync("ETHUSDT", 10);

var order = await restClient.FuturesV3Api.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 0.1m);

// In Hedge mode add positionSide: PositionSide.Long / PositionSide.Short.
```

## Core Pattern: WebSocket Subscriptions

Use `AsterSocketClient`. Always store the `UpdateSubscription` and unsubscribe when done.

```csharp
var socketClient = new AsterSocketClient();

var subscription = await socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success) { Console.WriteLine(subscription.Error); return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use unified shared interfaces. Same pattern works against Aster, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

```csharp
using Aster.Net.Clients;
using CryptoExchange.Net.SharedApis;

var asterShared = new AsterRestClient().SpotV3Api.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await asterShared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

## Dependency Injection

```csharp
using Aster.Net;
using Microsoft.Extensions.DependencyInjection;

services.AddAster(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
});
```

Inject `IAsterRestClient` and `IAsterSocketClient`.

## Common Pitfalls - AVOID

- Do not use raw `HttpClient` to call Aster endpoints.
- Do not use generic `ApiCredentials`; use `AsterCredentials`.
- Do not use `new AsterCredentials("key", "secret")` for V3. That overload configures V1 HMAC credentials.
- Do not default to `SpotApi` or `FuturesApi`; use `SpotV3Api` and `FuturesV3Api`.
- Do not pass a custom `clientOrderId` unless required.
- Do not mix sync and async. Always `await` async methods.
- Do not instantiate clients per request.
- Do not forget to unsubscribe from WebSocket streams.
- Do not assume `HttpResult.Data` is non-null without checking `.Success`.

## Environments

```csharp
var live = new AsterRestClient(o => o.Environment = AsterEnvironment.Live);
```

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/Aster.Net/
- Examples: `Examples/ai-friendly/`
- Source: https://github.com/JKorf/Aster.Net
- NuGet: https://www.nuget.org/packages/Jkorf.Aster.Net
- Discord: https://discord.gg/MSpeEtSY8t

