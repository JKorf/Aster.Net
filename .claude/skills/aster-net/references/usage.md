# Aster.Net usage

These snippets use the imports below. Account and trading snippets assume an authenticated `client`. See [API surfaces](api-surfaces.md) for method routing and [operational considerations](safety.md) for trading and retry decisions.

```csharp
using System;
using System.Threading.Tasks;
using Aster.Net;
using Aster.Net.Clients;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Web3;
using Microsoft.Extensions.DependencyInjection;
```

## Authentication

Public calls can use `new AsterRestClient()`. Private V3 calls require `EthKey` values, not string arguments:

```csharp
using var client = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials().WithV3(
        EthKey.FromPrivateKey("USER_PRIVATE_KEY"),
        EthKey.FromPrivateKey("SIGNER_PRIVATE_KEY"));
});
```

The key strings are placeholders. For key roles and storage, see [credentials](safety.md#credentials).

## REST results

```csharp
using var client = new AsterRestClient();
var ticker = await client.SpotV3Api.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}

Console.WriteLine(ticker.Data.LastPrice);
```

On failure, diagnostic fields include `ErrorCode`, `Message`, `ErrorType`, and `IsTransient`. See [retry decisions](safety.md#retry-decisions) before adding retries.

## Account and spot orders

Read balances with `client.SpotV3Api.Account.GetAccountInfoAsync()`; its result has `Data.Balances` containing `Asset`, `Free`, and `Locked`.

```csharp
var order = await client.SpotV3Api.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 50000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.Id);
```

Query or cancel with `GetOrderAsync(symbol, orderId: ...)` or `CancelOrderAsync(symbol, orderId: ...)`. Inspect each result. The [API map](api-surfaces.md#spot-v3-rest) lists related operations.

## Futures

```csharp
const string symbol = "ETHUSDT";
var leverage = await client.FuturesV3Api.Account.SetLeverageAsync(symbol, 5);
if (!leverage.Success)
{
    Console.WriteLine(leverage.Error);
    return;
}

var order = await client.FuturesV3Api.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 0.01m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.Id);
```

Use `positionSide` for hedge-mode orders and `SetMarginTypeAsync(symbol, MarginType.Isolated)` for margin configuration. Check the account's position mode before constructing a close; see [trading and account changes](safety.md#trading-and-account-changes).

## WebSocket lifecycle

```csharp
using var socket = new AsterSocketClient();
var subscription = await socket.SpotV3Api.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}

try
{
    Console.ReadLine(); // Replace with the application's lifetime.
}
finally
{
    await socket.UnsubscribeAsync(subscription.Data);
}
```

For klines, `SubscribeToKlineUpdatesAsync(symbol, interval, handler)` delivers the candle at `update.Data.Data`; `Final` indicates a closed candle. Futures mark-price subscriptions require an `updateInterval` argument. See [socket routing](api-surfaces.md#futures-v3-websocket).

## Shared API V2

```csharp
using var client = new AsterRestClient();
IGetTickerRest ticker = client.SpotV3Api.SharedApi;
var result = await ticker.GetTickerAsync(
    new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));

if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

For aggregate properties and runtime capability lookup, see [Shared APIs](api-surfaces.md#shared-api-v2).

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddAster();
```

For private calls, configure `options.ApiCredentials` in the `AddAster(options => ...)` callback with the credentials shown in [authentication](#authentication). Inject `IAsterRestClient`, `IAsterSocketClient`, or `IAsterSharedApiClient`.

## Complete examples in a source checkout

These links require the repository checkout; the snippets above are available in the packaged skill itself.

- [Spot quickstart](../../../../Examples/ai-friendly/01-spot-quickstart.cs)
- [Futures](../../../../Examples/ai-friendly/02-futures.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Multi-exchange](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
