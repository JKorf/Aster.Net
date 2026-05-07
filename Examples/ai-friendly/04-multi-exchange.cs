// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against Aster and other exchanges from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package Jkorf.Aster.Net
//   dotnet add package Binance.Net  // optional, for comparison

using Aster.Net.Clients;
using CryptoExchange.Net.SharedApis;

// Each exchange client exposes a `.SharedClient` property on its API surfaces.
// For Aster, prefer the V3 API branches.
ISpotTickerRestClient asterShared = new AsterRestClient().SpotV3Api.SharedClient;

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(asterShared, btcusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// REST shared interfaces include:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IBalanceRestClient
//   IPositionRestClient, IFeeRestClient, IOrderBookRestClient
//   IRecentTradeRestClient, IKlineRestClient, ITransferRestClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var asterSocket = new AsterSocketClient();
ITickerSocketClient asterTickerSocket = asterSocket.SpotV3Api.SharedClient;

var sub = await asterTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{asterTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await asterSocket.UnsubscribeAsync(sub.Data);

// Note: shared socket interfaces do not expose UnsubscribeAsync.
// Keep the concrete socket client and call concreteClient.UnsubscribeAsync(sub.Data).

