using System;
using System.Text.Json.Serialization;
using Aster.Net.Enums;

namespace Aster.Net.Objects.Models;

/// <summary>
/// Strategy order result
/// </summary>
public record AsterStrategyOrderResult
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
    public StrategyStatus StrategyStatus { get; set; }
    /// <summary>
    /// ["<c>updateTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("updateTime")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// ["<c>failureCode</c>"] Failure code
    /// </summary>
    [JsonPropertyName("failureCode")]
    public int FailureCode { get; set; }
    /// <summary>
    /// ["<c>failureReason</c>"] Failure reason
    /// </summary>
    [JsonPropertyName("failureReason")]
    public string? FailureReason { get; set; }
}

