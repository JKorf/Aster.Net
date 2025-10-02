
using Aster.Net.Clients;

// REST
var restClient = new AsterRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ASTERUSDT");
Console.WriteLine($"Rest client ticker price for ASTERUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new AsterSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ASTERUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ASTERUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
