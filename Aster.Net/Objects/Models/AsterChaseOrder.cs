using System;
using System.Text.Json.Serialization;
using Aster.Net.Enums;

namespace Aster.Net.Objects.Models;

/// <summary>
/// Chase order
/// </summary>
public record AsterChaseOrder
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
    public PositionSide PositionSide { get; set; }
    /// <summary>
    /// ["<c>quantity</c>"] Quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>quantityUnit</c>"] Quantity unit
    /// </summary>
    [JsonPropertyName("quantityUnit")]
    public QuantityUnit QuantityUnit { get; set; }
    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }
    /// <summary>
    /// ["<c>chaseOffset</c>"] Chase offset
    /// </summary>
    [JsonPropertyName("chaseOffset")]
    public decimal? ChaseOffset { get; set; }
    /// <summary>
    /// ["<c>chaseOffsetType</c>"] Chase offset type
    /// </summary>
    [JsonPropertyName("chaseOffsetType")]
    public ChaseOffsetType? ChaseOffsetType { get; set; }
    /// <summary>
    /// ["<c>maxChaseOffset</c>"] Max chase offset
    /// </summary>
    [JsonPropertyName("maxChaseOffset")]
    public decimal? MaxChaseOffset { get; set; }
    /// <summary>
    /// ["<c>maxChaseOffsetType</c>"] Max chase offset type
    /// </summary>
    [JsonPropertyName("maxChaseOffsetType")]
    public ChaseOffsetType? MaxChaseOffsetType { get; set; }
    /// <summary>
    /// ["<c>priceLimit</c>"] Price limit
    /// </summary>
    [JsonPropertyName("priceLimit")]
    public decimal? PriceLimit { get; set; }
    /// <summary>
    /// ["<c>timeInForce</c>"] Time in force
    /// </summary>
    [JsonPropertyName("timeInForce")]
    public TimeInForce TimeInForce { get; set; }
    /// <summary>
    /// ["<c>strategyStatus</c>"] Strategy status
    /// </summary>
    [JsonPropertyName("strategyStatus")]
    public OrderStatus StrategyStatus { get; set; }
    /// <summary>
    /// ["<c>bookTime</c>"] Order book entry time
    /// </summary>
    [JsonPropertyName("bookTime")]
    public DateTime BookTime { get; set; }
    /// <summary>
    /// ["<c>updateTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("updateTime")]
    public DateTime? UpdateTime { get; set; }
}

