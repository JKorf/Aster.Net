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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionAmt</c>"] Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal PositionAmount { get; set; }
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        [JsonInclude, JsonPropertyName("unRealizedProfit")]
        internal decimal UnrealizedProfitInt
        {
            set => UnrealizedProfit = value;
        }
        /// <summary>
        /// ["<c>isolated</c>"] Is isolated margin
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>positionInitialMargin</c>"] Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>openOrderInitialMargin</c>"] Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal? AverageEntryPrice { get; set; }
        /// <summary>
        /// ["<c>liquidationPrice</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal? IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>maxNotional</c>"] Maximum available notional with current leverage
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal? MaxAvailableNotional { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal? MaxNotionalValue { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal? Notional { get; set; }
        /// <summary>
        /// ["<c>isolatedWallet</c>"] IsolatedWallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal? IsolatedWallet { get; set; }
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>isAutoAddMargin</c>"] Is auto-add margin
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
