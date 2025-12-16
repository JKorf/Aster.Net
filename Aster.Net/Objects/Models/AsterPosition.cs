using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public record AsterPosition
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal PositionAmount { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        [JsonInclude, JsonPropertyName("unRealizedProfit")]
        internal decimal UnrealizedProfitInt
        {
            set => UnrealizedProfit = value;
        }
        /// <summary>
        /// Is isolated margin
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal? AverageEntryPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal? IsolatedMargin { get; set; }
        /// <summary>
        /// Maximum available notional with current leverage
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal? MaxAvailableNotional { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal? MaxNotionalValue { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// Is auto-add margin
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
