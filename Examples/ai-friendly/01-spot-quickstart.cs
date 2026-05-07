// 01-spot-quickstart.cs
//
// Demonstrates: V3 client setup, public market data, authenticated balance,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package Jkorf.Aster.Net
//   Copy this file content into Program.cs
//   Substitute USER_PRIVATE_KEY / SIGNER_PRIVATE_KEY below
//   dotnet run

using Aster.Net;
using Aster.Net.Clients;
using Aster.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create per request.
var publicClient = new AsterRestClient();

var ticker = await publicClient.SpotV3Api.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"BTC/USDT last price: {ticker.Data.LastPrice}");
Console.WriteLine($"24h volume: {ticker.Data.Volume} BTC");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
// Aster V3 uses a user private key and signer private key.
var tradingClient = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
});

var account = await tradingClient.SpotV3Api.Account.GetAccountInfoAsync();
if (!account.Success)
{
    Console.WriteLine($"Failed to get account: {account.Error}");
    return;
}

foreach (var balance in account.Data.Balances.Where(b => b.Free + b.Locked > 0))
{
    Console.WriteLine($"{balance.Asset}: {balance.Free} free, {balance.Locked} locked");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.001 BTC at a price 5% below current, likely not filled immediately.
// Let Aster.Net auto-generate clientOrderId unless you have a specific need.
var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);

var order = await tradingClient.SpotV3Api.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: safePrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id} at {safePrice}, status: {order.Data.Status}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotV3Api.Trading.GetOrderAsync("BTCUSDT", order.Data.Id);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotV3Api.Trading.CancelOrderAsync("BTCUSDT", order.Data.Id);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.Id}");
}

// Common variations:
//   Market order: type: OrderType.Market, omit price and timeInForce
//   Stop order:   type: OrderType.Stop, add stopPrice parameter
//   Quote amount: use quoteQuantity parameter instead of quantity for market buys

