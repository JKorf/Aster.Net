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

// SharedApi exposes capabilities for individual operations.
using var asterRest = new AsterRestClient();
IGetTickerRest asterShared = asterRest.SpotV3Api.SharedApi;

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(asterShared, btcusdt);

async Task PrintTicker(IGetTickerRest client, SharedSymbol symbol)
{
    var result = await client.GetTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// Other capabilities include IGetSpotSymbolsRest, IPlaceSpotOrderRest,
// IPlaceFuturesOrderRest, IGetBalancesRest, and IGetOrderBookRest.

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
using var asterSocket = new AsterSocketClient();
ISubscribeTickerSocket asterTickerSocket = asterSocket.SpotV3Api.SharedApi;

var sub = await asterTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{asterTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

try
{
    Console.WriteLine("Press Enter to exit");
    Console.ReadLine();
}
finally
{
    await asterSocket.UnsubscribeAsync(sub.Data);
}

// Note: shared socket interfaces do not expose UnsubscribeAsync.
// Keep the concrete socket client and call concreteClient.UnsubscribeAsync(sub.Data).

