# Aster.Net API surfaces

Use this reference to route an intent to the relevant member. Table entries abbreviate overloads; confirm parameters in the installed package before use. For setup and result examples, see [usage](usage.md).

## Client roots

| Surface | Purpose |
| --- | --- |
| `AsterRestClient.SpotV3Api.ExchangeData` | Spot market data |
| `AsterRestClient.SpotV3Api.Account` | Spot balances, user streams, transfers, and commission rates |
| `AsterRestClient.SpotV3Api.Trading` | Spot orders and user trades |
| `AsterRestClient.FuturesV3Api.ExchangeData` | Futures market data |
| `AsterRestClient.FuturesV3Api.Account` | Futures account, leverage, margin, and position mode |
| `AsterRestClient.FuturesV3Api.Trading` | Futures orders, positions, and strategies |
| `AsterSocketClient.SpotV3Api` | Spot subscriptions |
| `AsterSocketClient.FuturesV3Api` | Futures subscriptions |

Aster.Net 5.x has no V1 client branches. The tables below use `client` for an `AsterRestClient` and `socketClient` for an `AsterSocketClient` instance.

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

## Futures strategy orders

The futures trading surface also exposes `PlaceChaseOrderAsync`, `PlaceStrategyOrderAsync`, `EditStrategyOrderAsync`, `GetOpenStrategyOrderAsync`, and `GetClosedStrategyOrderAsync`. For multi-leg responses, inspect nested results as well as outer success.

## Shared API V2

For portable code, use `.SharedApi` on the selected native REST or socket root. Keep native request/model types separate from Shared API types.

| Intent | Capability or member |
| --- | --- |
| Get a ticker | `IGetTickerRest.GetTickerAsync` |
| Get/filter spot symbols | `IGetSpotSymbolsRest.GetSpotSymbolsAsync` |
| Get/filter futures symbols | `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync` |
| Place a spot order | `IPlaceSpotOrderRest.PlaceSpotOrderAsync` |
| Place a futures order | `IPlaceFuturesOrderRest.PlaceFuturesOrderAsync` |
| Subscribe to tickers | `ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync` |
| Subscribe to trades | `ISubscribeTradesSocket.SubscribeToTradeUpdatesAsync` |
| Subscribe to order books | `ISubscribeOrderBookSocket.SubscribeToOrderBookUpdatesAsync` |

`IAsterSharedApiClient` groups the four supported surfaces:

| Property | Interface |
| --- | --- |
| `SpotV3Rest` | `IAsterRestClientSpotV3SharedApi` |
| `FuturesV3Rest` | `IAsterRestClientFuturesV3SharedApi` |
| `SpotV3Socket` | `IAsterSocketClientSpotV3SharedApi` |
| `FuturesV3Socket` | `IAsterSocketClientFuturesV3SharedApi` |

Use a surface property when the operation is known. For dynamic selection, call `GetCapability` on an aggregate instance with a typed descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest`, and specify the intended trading mode when selecting between spot and futures. Check for a null match, then use its `Capability` and `Options`. Use `GetCapabilities` for all matches and `Discover` for summary metadata.

`AddAster` registers the aggregate and its supported capabilities. Where several surfaces or exchanges implement an injected capability, select the intended implementation explicitly instead of relying on the last DI registration.

### Symbols and quantities

Call `GetSpotSymbolsAsync` or `GetFuturesSymbolsAsync` successfully before reading the corresponding `SpotSymbolCatalog` or `FuturesSymbolCatalog`. `GetSymbolsRequest` supports asset-type filters. Symbol responses populate display names, base/quote asset types, and subtypes; futures can include stock and commodity underlyings.

Shared book ticker, position, and user-trade quantities use `SharedOrderQuantity`. Read `QuantityInBaseAsset`, `QuantityInQuoteAsset`, or `QuantityInContracts` as appropriate. Aster shared order books report `QuantityType` as `SharedQuantityType.BaseAsset`.

Shared subscriptions are closed through the concrete `AsterSocketClient.UnsubscribeAsync(subscription.Data)`.

## Source navigation in a checkout

These links are available in a source checkout, not in the standalone NuGet skill:

- [Client interfaces](../../../../Aster.Net/Interfaces/Clients/): exact method signatures and endpoint documentation.
- [Client implementations](../../../../Aster.Net/Clients/): transport and Shared API mappings.
- [Compiled examples](../../../../Examples/ai-friendly/): complete programs.

For packaged usage patterns, see [usage](usage.md).
