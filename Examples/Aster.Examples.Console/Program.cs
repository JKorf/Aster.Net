
using Aster.Net.Clients;

// REST
var restClient = new AsterRestClient();
var ticker = await restClient.SpotV3Api.ExchangeData.GetTickerAsync("ASTERUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ASTERUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new AsterSocketClient();
var subscription = await socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync("ASTERUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ASTERUSDT: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
