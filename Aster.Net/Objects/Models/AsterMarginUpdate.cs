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
        /// ["<c>cw</c>"] Cross Wallet Balance. Only pushed with crossed position margin call
        /// </summary>
        [JsonPropertyName("cw")]
        public decimal? CrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Positions
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
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>ps</c>"] Position Side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// ["<c>pa</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("pa")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// ["<c>mt</c>"] Margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType MarginType { get; set; }

        /// <summary>
        /// ["<c>iw</c>"] Isolated Wallet (if isolated position)
        /// </summary>
        [JsonPropertyName("iw")]
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// ["<c>mp</c>"] Mark Price
        /// </summary>
        [JsonPropertyName("mp")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// ["<c>up</c>"] Unrealized PnL
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>mm</c>"] Maintenance Margin Required
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal MaintenanceMargin { get; set; }
    }
}
