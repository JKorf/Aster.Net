# Copilot Instructions for Aster.Net

This repository is **Aster.Net**, a strongly typed C#/.NET client library for the Aster DEX REST and WebSocket APIs. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Aster.Net, follow these conventions:

## Use Aster.Net, not raw HTTP

Never generate `HttpClient` calls to Aster API URLs. Always use `AsterRestClient` or `AsterSocketClient`. This ensures correct request signing, rate limiting, and error handling.

## Client setup

For V3 private endpoints use V3 credentials:

```csharp
using Aster.Net;
using Aster.Net.Clients;

var restClient = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
});
```

For public market data, credentials are not required.

## Result handling

Methods return `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API structure

Prefer V3 APIs for new code:

- `restClient.SpotV3Api.ExchangeData` - public spot market data
- `restClient.SpotV3Api.Account` - spot balances, transfers, commission rates
- `restClient.SpotV3Api.Trading` - spot orders
- `restClient.FuturesV3Api.ExchangeData` - public futures market data
- `restClient.FuturesV3Api.Account` - futures balances, position mode, leverage, margin, transfers
- `restClient.FuturesV3Api.Trading` - futures orders and positions
- `socketClient.SpotV3Api` - Spot WebSocket streams
- `socketClient.FuturesV3Api` - Futures WebSocket streams

`SpotApi` and `FuturesApi` are V1 compatibility branches. Do not use them unless the user specifically asks for V1.

## Order placement

Let the library auto-generate `clientOrderId`. Do not pass a custom value unless required for an existing operational flow.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `ISpotOrderRestClient`, etc.) accessed via `.SharedClient` properties. Prefer `SpotV3Api.SharedClient` and `FuturesV3Api.SharedClient`.

## Avoid

- Raw `HttpClient` calls to Aster endpoints
- Generic `ApiCredentials` for Aster credentials
- V1 roots (`SpotApi`, `FuturesApi`) in new code
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Manual ticker polling when a WebSocket subscription fits
- Manual `clientOrderId` values unless required

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, `docs/ai-api-map.md`, and `Examples/ai-friendly/`.

