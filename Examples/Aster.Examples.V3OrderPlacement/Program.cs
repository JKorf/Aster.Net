using Aster.Net;
using Aster.Net.Clients;
using Aster.Net.Enums;

const string spotSymbol = "BTCUSDT";
const string futuresSymbol = "ETHUSDT";

// Replace with valid credentials or order placement will always fail
var userPrivateKey = "KEY";
var signerPrivateKey = "SIGNERKEY";

Console.WriteLine("Aster.Net V3 order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3(userPrivateKey, signerPrivateKey);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(AsterRestClient client)
{
    Console.WriteLine($"Placing spot V3 limit buy order for {spotSymbol}...");

    var ticker = await client.SpotV3Api.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);
    var order = await client.SpotV3Api.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: OrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.SpotV3Api.Trading.GetOrderAsync(spotSymbol, order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotV3Api.Trading.CancelOrderAsync(spotSymbol, order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.Id}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(AsterRestClient client)
{
    Console.WriteLine($"Placing futures V3 reduce-only limit sell order for {futuresSymbol}...");

    var ticker = await client.FuturesV3Api.ExchangeData.GetTickerAsync(futuresSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 1.05m, 2);
    var order = await client.FuturesV3Api.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: OrderSide.Sell,
        type: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.Id}, status: {order.Data.Status}");

    var orderStatus = await client.FuturesV3Api.Trading.GetOrderAsync(futuresSymbol, order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.FuturesV3Api.Trading.CancelOrderAsync(futuresSymbol, order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.Id}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
