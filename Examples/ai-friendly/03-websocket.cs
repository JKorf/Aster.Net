// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions - public ticker and klines.
// Includes proper teardown.
//
// Setup: dotnet add package Jkorf.Aster.Net

using Aster.Net.Clients;
using Aster.Net.Enums;

// Reuse a single client instance across subscriptions.
var socketClient = new AsterSocketClient();

var tickerSub = await socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update =>
    {
        Console.WriteLine($"BTC: {update.Data.LastPrice} (24h vol {update.Data.Volume:F2})");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await socketClient.SpotV3Api.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    KlineInterval.OneMinute,
    update =>
    {
        var k = update.Data.Data;
        if (k.Final)
        {
            Console.WriteLine($"ETH 1m closed: O={k.OpenPrice} H={k.HighPrice} L={k.LowPrice} C={k.ClosePrice}");
        }
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await socketClient.UnsubscribeAsync(tickerSub.Data);
    return;
}

Console.WriteLine("Subscriptions active. Press Enter to teardown...");
Console.ReadLine();

await socketClient.UnsubscribeAsync(tickerSub.Data);
await socketClient.UnsubscribeAsync(klineSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple symbols: socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync(new[] { "BTCUSDT", "ETHUSDT" }, handler)
//   Order book:       socketClient.SpotV3Api.SubscribeToOrderBookUpdatesAsync(symbol, updateInterval, handler)
//   Futures ticker:   socketClient.FuturesV3Api.SubscribeToTickerUpdatesAsync(symbol, handler)
//   Mark price:       socketClient.FuturesV3Api.SubscribeToMarkPriceUpdatesAsync(symbol, updateInterval, handler)

