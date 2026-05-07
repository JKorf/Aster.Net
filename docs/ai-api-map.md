# Aster.Net AI API Quick Map

Use this file to route common user intents to the correct Aster.Net client member. Prefer V3 APIs. If a method name or parameter is not listed here, inspect `Aster.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new AsterRestClient()` |
| WebSocket streams | `new AsterSocketClient()` |
| V3 API authentication | `options.ApiCredentials = new AsterCredentials().WithV3("userPrivateKey", "signerPrivateKey")` |
| Live environment | `AsterEnvironment.Live` |
| Dependency injection | `services.AddAster(options => { ... })` |
| Spot V3 REST | `client.SpotV3Api` |
| Futures V3 REST | `client.FuturesV3Api` |
| Spot V3 socket | `socketClient.SpotV3Api` |
| Futures V3 socket | `socketClient.FuturesV3Api` |

## Spot V3 REST

| User intent | Aster.Net member |
|---|---|
| Get server time | `client.SpotV3Api.ExchangeData.GetServerTimeAsync()` |
| Get spot exchange info | `client.SpotV3Api.ExchangeData.GetExchangeInfoAsync()` |
| Get latest spot ticker | `client.SpotV3Api.ExchangeData.GetTickerAsync("BTCUSDT")` |
| Get all spot tickers | `client.SpotV3Api.ExchangeData.GetTickersAsync()` |
| Get spot price | `client.SpotV3Api.ExchangeData.GetPriceAsync("BTCUSDT")` |
| Get all spot prices | `client.SpotV3Api.ExchangeData.GetPricesAsync()` |
| Get spot order book | `client.SpotV3Api.ExchangeData.GetOrderBookAsync("BTCUSDT")` |
| Get recent trades | `client.SpotV3Api.ExchangeData.GetRecentTradesAsync("BTCUSDT")` |
| Get historical trades | `client.SpotV3Api.ExchangeData.GetTradeHistoryAsync("BTCUSDT")` |
| Get aggregate trades | `client.SpotV3Api.ExchangeData.GetAggregatedTradeHistoryAsync("BTCUSDT")` |
| Get klines/candles | `client.SpotV3Api.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute)` |
| Get book ticker | `client.SpotV3Api.ExchangeData.GetBookTickerAsync("BTCUSDT")` |
| Get book tickers | `client.SpotV3Api.ExchangeData.GetBookTickersAsync()` |
| Get account info and balances | `client.SpotV3Api.Account.GetAccountInfoAsync()` |
| Get user commission rate | `client.SpotV3Api.Account.GetUserCommissionRateAsync("BTCUSDT")` |
| Transfer between spot and futures | `client.SpotV3Api.Account.TransferAsync(...)` |
| Start spot user stream | `client.SpotV3Api.Account.StartUserStreamAsync()` |
| Keep alive spot user stream | `client.SpotV3Api.Account.KeepAliveUserStreamAsync(listenKey)` |
| Stop spot user stream | `client.SpotV3Api.Account.StopUserStreamAsync(listenKey)` |
| Place spot order | `client.SpotV3Api.Trading.PlaceOrderAsync(...)` |
| Query spot order | `client.SpotV3Api.Trading.GetOrderAsync(symbol, orderId)` |
| Get open spot orders | `client.SpotV3Api.Trading.GetOpenOrdersAsync(symbol)` |
| Get all spot orders | `client.SpotV3Api.Trading.GetOrdersAsync(symbol)` |
| Cancel spot order | `client.SpotV3Api.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel all spot orders | `client.SpotV3Api.Trading.CancelAllOrdersAsync(symbol)` |
| Get spot user trades | `client.SpotV3Api.Trading.GetUserTradesAsync(symbol)` |

## Futures V3 REST

| User intent | Aster.Net member |
|---|---|
| Get futures exchange info | `client.FuturesV3Api.ExchangeData.GetExchangeInfoAsync()` |
| Get futures server time | `client.FuturesV3Api.ExchangeData.GetServerTimeAsync()` |
| Get futures ticker | `client.FuturesV3Api.ExchangeData.GetTickerAsync("ETHUSDT")` |
| Get all futures tickers | `client.FuturesV3Api.ExchangeData.GetTickersAsync()` |
| Get futures price | `client.FuturesV3Api.ExchangeData.GetPriceAsync("ETHUSDT")` |
| Get all futures prices | `client.FuturesV3Api.ExchangeData.GetPricesAsync()` |
| Get futures order book | `client.FuturesV3Api.ExchangeData.GetOrderBookAsync("ETHUSDT")` |
| Get futures klines | `client.FuturesV3Api.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get mark price | `client.FuturesV3Api.ExchangeData.GetMarkPriceAsync("ETHUSDT")` |
| Get all mark prices | `client.FuturesV3Api.ExchangeData.GetMarkPricesAsync()` |
| Get mark price klines | `client.FuturesV3Api.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get index price klines | `client.FuturesV3Api.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get funding rates | `client.FuturesV3Api.ExchangeData.GetFundingRatesAsync("ETHUSDT")` |
| Get funding info | `client.FuturesV3Api.ExchangeData.GetFundingInfoAsync()` |
| Get book ticker | `client.FuturesV3Api.ExchangeData.GetBookTickerAsync("ETHUSDT")` |
| Get account info | `client.FuturesV3Api.Account.GetAccountInfoAsync()` |
| Get balances | `client.FuturesV3Api.Account.GetBalancesAsync()` |
| Set leverage | `client.FuturesV3Api.Account.SetLeverageAsync(symbol, leverage)` |
| Set margin type | `client.FuturesV3Api.Account.SetMarginTypeAsync(symbol, MarginType.Isolated)` |
| Get position mode | `client.FuturesV3Api.Account.GetPositionModeAsync()` |
| Set position mode | `client.FuturesV3Api.Account.SetPositionModeAsync(...)` |
| Get multi-asset mode | `client.FuturesV3Api.Account.GetMultiAssetModeAsync()` |
| Set multi-asset mode | `client.FuturesV3Api.Account.SetMultiAssetModeAsync(...)` |
| Modify isolated margin | `client.FuturesV3Api.Account.ModifyIsolatedMarginAsync(...)` |
| Get position margin history | `client.FuturesV3Api.Account.GetPositionMarginChangeHistoryAsync(symbol)` |
| Get income history | `client.FuturesV3Api.Account.GetIncomeHistoryAsync(...)` |
| Get leverage brackets | `client.FuturesV3Api.Account.GetLeverageBracketsAsync(symbol)` |
| Get ADL quantile estimation | `client.FuturesV3Api.Account.GetPositionAdlQuantileEstimationAsync(symbol)` |
| Get futures commission rate | `client.FuturesV3Api.Account.GetUserCommissionRateAsync(symbol)` |
| Transfer between futures and spot | `client.FuturesV3Api.Account.TransferAsync(...)` |
| Get withdraw info | `client.FuturesV3Api.Account.GetWithdrawInfoAsync()` |
| Get deposit/withdraw history | `client.FuturesV3Api.Account.GetDepositWithdrawHistoryAsync()` |
| Approve builder | `client.FuturesV3Api.Account.ApproveBuilderAsync(...)` |
| Get approved builders | `client.FuturesV3Api.Account.GetApprovedBuildersAsync()` |
| Create or approve agent | `client.FuturesV3Api.Account.CreateOrApproveAgentAsync(...)` |
| Get agents | `client.FuturesV3Api.Account.GetAgentsAsync()` |
| Place futures order | `client.FuturesV3Api.Trading.PlaceOrderAsync(...)` |
| Place multiple futures orders | `client.FuturesV3Api.Trading.PlaceMultipleOrdersAsync(...)` |
| Query futures order | `client.FuturesV3Api.Trading.GetOrderAsync(symbol, orderId)` |
| Cancel futures order | `client.FuturesV3Api.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel all futures orders | `client.FuturesV3Api.Trading.CancelAllOrdersAsync(symbol)` |
| Cancel multiple futures orders | `client.FuturesV3Api.Trading.CancelMultipleOrdersAsync(...)` |
| Dead man's switch | `client.FuturesV3Api.Trading.CancelAllOrdersAfterTimeoutAsync(...)` |
| Get open futures orders | `client.FuturesV3Api.Trading.GetOpenOrdersAsync(symbol)` |
| Get all futures orders | `client.FuturesV3Api.Trading.GetOrdersAsync(symbol)` |
| Get positions | `client.FuturesV3Api.Trading.GetPositionsAsync(symbol)` |
| Get futures user trades | `client.FuturesV3Api.Trading.GetUserTradesAsync(symbol)` |
| Get forced orders | `client.FuturesV3Api.Trading.GetForcedOrdersAsync(symbol)` |

## Spot V3 WebSocket

| User intent | Aster.Net member |
|---|---|
| Subscribe spot ticker updates | `socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe all spot ticker updates | `socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync(handler)` |
| Subscribe spot mini ticker updates | `socketClient.SpotV3Api.SubscribeToMiniTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot klines | `socketClient.SpotV3Api.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe spot order book snapshots | `socketClient.SpotV3Api.SubscribeToPartialOrderBookUpdatesAsync(...)` |
| Subscribe spot order book updates | `socketClient.SpotV3Api.SubscribeToOrderBookUpdatesAsync(...)` |
| Subscribe spot book ticker | `socketClient.SpotV3Api.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot aggregate trades | `socketClient.SpotV3Api.SubscribeToAggregatedTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot user data | `socketClient.SpotV3Api.SubscribeToUserDataUpdatesAsync(...)` |

## Futures V3 WebSocket

| User intent | Aster.Net member |
|---|---|
| Subscribe futures ticker updates | `socketClient.FuturesV3Api.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe futures klines | `socketClient.FuturesV3Api.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe futures book ticker | `socketClient.FuturesV3Api.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe futures aggregate trades | `socketClient.FuturesV3Api.SubscribeToAggregatedTradeUpdatesAsync(symbol, handler)` |
| Subscribe futures order book updates | `socketClient.FuturesV3Api.SubscribeToOrderBookUpdatesAsync(...)` |
| Subscribe futures mark price | `socketClient.FuturesV3Api.SubscribeToMarkPriceUpdatesAsync(symbol, updateInterval, handler)` |
| Subscribe futures liquidation updates | `socketClient.FuturesV3Api.SubscribeToLiquidationUpdatesAsync(symbol, handler)` |
| Subscribe futures user data | `socketClient.FuturesV3Api.SubscribeToUserDataUpdatesAsync(...)` |

## SharedApis

| User intent | Aster.Net member or interface |
|---|---|
| Shared spot REST client | `new AsterRestClient().SpotV3Api.SharedClient` |
| Shared futures REST client | `new AsterRestClient().FuturesV3Api.SharedClient` |
| Shared spot socket client | `new AsterSocketClient().SpotV3Api.SharedClient` |
| Shared futures socket client | `new AsterSocketClient().FuturesV3Api.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| Raw `HttpClient` | `AsterRestClient` / `AsterSocketClient` |
| `ApiCredentials` | `AsterCredentials` |
| `new AsterCredentials("key", "secret")` for V3 | `new AsterCredentials().WithV3(...)` |
| `SpotApi` for new code | `SpotV3Api` |
| `FuturesApi` for new code | `FuturesV3Api` |
| `.Data` without `.Success` check | Check `.Success` first |
| Shared socket `UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Custom `clientOrderId` by default | Let Aster.Net auto-generate it |
| `positionSide` in every futures order | Include only when hedge mode is intended |

