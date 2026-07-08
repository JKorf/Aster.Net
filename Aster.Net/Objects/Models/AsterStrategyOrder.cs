using System;
using System.Text.Json.Serialization;
using Aster.Net.Enums;

namespace Aster.Net.Objects.Models;

/// <summary>
/// Strategy order
/// </summary>
public record AsterStrategyOrder
{
    /// <summary>
    /// ["<c>strategyId</c>"] Strategy id
    /// </summary>
    [JsonPropertyName("strategyId")]
    public long StrategyId { get; set; }
    /// <summary>
    /// ["<c>clientStrategyId</c>"] Client strategy id
    /// </summary>
    [JsonPropertyName("clientStrategyId")]
    public string? ClientStrategyId { get; set; }
    /// <summary>
    /// ["<c>strategyType</c>"] Strategy type
    /// </summary>
    [JsonPropertyName("strategyType")]
    public StrategyType StrategyType { get; set; }
    /// <summary>
    /// ["<c>strategyStatus</c>"] Strategy status
    /// </summary>
    [JsonPropertyName("strategyStatus")]
    public StrategyStatus Status { get; set; }
    /// <summary>
    /// ["<c>bookTime</c>"] Book time
    /// </summary>
    [JsonPropertyName("bookTime")]
    public DateTime BookTime { get; set; }
    /// <summary>
    /// ["<c>updateTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("updateTime")]
    public DateTime UpdateTime { get; set; }
    /// <summary>
    /// ["<c>subOrders</c>"] Sub orders
    /// </summary>
    [JsonPropertyName("subOrders")]
    public AsterStrategyOrderSub[] SubOrders { get; set; } = [];
}

/// <summary>
/// Strategy sub order
/// </summary>
public record AsterStrategyOrderSub
{
    /// <summary>
    /// ["<c>strategyId</c>"] Strategy id
    /// </summary>
    [JsonPropertyName("strategyId")]
    public long StrategyId { get; set; }
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>clientOrderId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clientOrderId")]
    public string? ClientOrderId { get; set; }
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }
    /// <summary>
    /// ["<c>strategySubId</c>"] Strategy sub id
    /// </summary>
    [JsonPropertyName("strategySubId")]
    public long StrategySubId { get; set; }
    /// <summary>
    /// ["<c>firstDrivenId</c>"] First driven id
    /// </summary>
    [JsonPropertyName("firstDrivenId")]
    public int? FirstDrivenId { get; set; }
    /// <summary>
    /// ["<c>firstDrivenOn</c>"] First driven on
    /// </summary>
    [JsonPropertyName("firstDrivenOn")]
    public OrderEventType? FirstDrivenOn { get; set; }
    /// <summary>
    /// ["<c>firstTrigger</c>"] First trigger
    /// </summary>
    [JsonPropertyName("firstTrigger")]
    public TriggerAction? FirstTrigger { get; set; }
    /// <summary>
    /// ["<c>secondDrivenId</c>"] Second driven id
    /// </summary>
    [JsonPropertyName("secondDrivenId")]
    public int? SecondDrivenId { get; set; }
    /// <summary>
    /// ["<c>secondDrivenOn</c>"] Second driven on
    /// </summary>
    [JsonPropertyName("secondDrivenOn")]
    public OrderEventType? SecondDrivenOn { get; set; }
    /// <summary>
    /// ["<c>secondTrigger</c>"] Second trigger
    /// </summary>
    [JsonPropertyName("secondTrigger")]
    public TriggerAction? SecondTrigger { get; set; }
    /// <summary>
    /// ["<c>securityType</c>"] Security type
    /// </summary>
    [JsonPropertyName("securityType")]
    public SecurityType SecurityType { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>positionSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("positionSide")]
    public PositionSide? PositionSide { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType Type { get; set; }
    /// <summary>
    /// ["<c>timeInForce</c>"] Time in force
    /// </summary>
    [JsonPropertyName("timeInForce")]
    public TimeInForce? TimeInForce { get; set; }
    /// <summary>
    /// ["<c>quantity</c>"] Quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }
    /// <summary>
    /// ["<c>closePosition</c>"] Close position
    /// </summary>
    [JsonPropertyName("closePosition")]
    public bool? ClosePosition { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>avgPrice</c>"] Average price
    /// </summary>
    [JsonPropertyName("avgPrice")]
    public decimal AveragePrice { get; set; }
    /// <summary>
    /// ["<c>priceProtect</c>"] Price protect
    /// </summary>
    [JsonPropertyName("priceProtect")]
    public bool PriceProtect { get; set; }
    /// <summary>
    /// ["<c>stopPrice</c>"] Stop price
    /// </summary>
    [JsonPropertyName("stopPrice")]
    public decimal StopPrice { get; set; }
    /// <summary>
    /// ["<c>activatePrice</c>"] Activate price
    /// </summary>
    [JsonPropertyName("activatePrice")]
    public decimal? ActivatePrice { get; set; }
    /// <summary>
    /// ["<c>callbackRate</c>"] Callback rate
    /// </summary>
    [JsonPropertyName("callbackRate")]
    public decimal? CallbackRate { get; set; }
    /// <summary>
    /// ["<c>workingType</c>"] Working type
    /// </summary>
    [JsonPropertyName("workingType")]
    public WorkingType? WorkingType { get; set; }
    /// <summary>
    /// ["<c>triggerTime</c>"] Trigger time
    /// </summary>
    [JsonPropertyName("triggerTime")]
    public DateTime? TriggerTime { get; set; }
}

