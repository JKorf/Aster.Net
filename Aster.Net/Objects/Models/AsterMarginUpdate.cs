using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Margin call update
    /// </summary>
    public record AsterMarginUpdate : AsterSocketEvent
    {

        /// <summary>
        /// Cross Wallet Balance. Only pushed with crossed position margin call
        /// </summary>
        [JsonPropertyName("cw")]
        public decimal? CrossWalletBalance { get; set; }
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("p")]
        public AsterMarginPosition[] Positions { get; set; } = Array.Empty<AsterMarginPosition>();

        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Position info
    /// </summary>
    public record AsterMarginPosition
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Position Side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("pa")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType MarginType { get; set; }

        /// <summary>
        /// Isolated Wallet (if isolated position)
        /// </summary>
        [JsonPropertyName("iw")]
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonPropertyName("mp")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Unrealized PnL
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Maintenance Margin Required
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal MaintenanceMargin { get; set; }
    }
}
