// 02-futures.cs
//
// Demonstrates: Futures V3 - set leverage, place market order,
// retrieve open position, close position.
//
// Setup: dotnet add package Jkorf.Aster.Net
// Substitute USER_PRIVATE_KEY / SIGNER_PRIVATE_KEY.

using Aster.Net;
using Aster.Net.Clients;
using Aster.Net.Enums;

var client = new AsterRestClient(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
});

const string symbol = "ETHUSDT";

// ---- 1. SET LEVERAGE ----
// Leverage is per-symbol and persists across orders.
var leverage = await client.FuturesV3Api.Account.SetLeverageAsync(symbol, 5);
if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}
Console.WriteLine($"Leverage set to {leverage.Data.Leverage}x for {symbol}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// In Hedge mode add positionSide: PositionSide.Long.
var openOrder = await client.FuturesV3Api.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 0.01m);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}
Console.WriteLine($"Opened position via order {openOrder.Data.Id}");

// ---- 3. GET CURRENT POSITION ----
var positions = await client.FuturesV3Api.Trading.GetPositionsAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

var position = positions.Data.FirstOrDefault(p => p.PositionAmount != 0);
if (position == null)
{
    Console.WriteLine("No open position found (may not have filled yet).");
    return;
}

Console.WriteLine($"Position: {position.PositionAmount} {symbol} at avg {position.AverageEntryPrice}");
Console.WriteLine($"Unrealized PnL: {position.UnrealizedProfit} USDT");
Console.WriteLine($"Liquidation price: {position.LiquidationPrice}");

// ---- 4. CLOSE THE POSITION ----
// Opposite side, same quantity, reduceOnly=true to avoid an accidental position flip.
var closeOrder = await client.FuturesV3Api.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Sell,
    type: OrderType.Market,
    quantity: Math.Abs(position.PositionAmount),
    reduceOnly: true);

if (closeOrder.Success)
{
    Console.WriteLine($"Closed position via order {closeOrder.Data.Id}");
}

// Common variations:
//   Limit order: type: OrderType.Limit, add price + timeInForce
//   Stop-market: type: OrderType.StopMarket, add stopPrice
//   Take-profit: type: OrderType.TakeProfitMarket, add stopPrice
//   Hedge mode: add positionSide: PositionSide.Long / PositionSide.Short
//   Margin type: client.FuturesV3Api.Account.SetMarginTypeAsync(symbol, MarginType.Isolated)

