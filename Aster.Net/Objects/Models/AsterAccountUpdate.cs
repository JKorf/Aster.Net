using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Account update
    /// </summary>
    public record AsterAccountUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>a</c>"] The update data
        /// </summary>
        [JsonPropertyName("a")]
        public AsterAccountUpdateData UpdateData { get; set; } = new AsterAccountUpdateData();
        /// <summary>
        /// ["<c>T</c>"] Transaction time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Account update data
    /// </summary>
    public record AsterAccountUpdateData
    {
        /// <summary>
        /// ["<c>m</c>"] Account update reason type
        /// </summary>
        [JsonPropertyName("m")]
        public AccountUpdateReason Reason { get; set; }

        /// <summary>
        /// ["<c>B</c>"] Balances
        /// </summary>
        [JsonPropertyName("B")]
        public AsterBalanceUpdate[] Balances { get; set; } = Array.Empty<AsterBalanceUpdate>();

        /// <summary>
        /// ["<c>P</c>"] Positions
        /// </summary>
        [JsonPropertyName("P")]
        public AsterPositionUpdate[] Positions { get; set; } = Array.Empty<AsterPositionUpdate>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record AsterBalanceUpdate
    {
        /// <summary>
        /// ["<c>a</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>wb</c>"] The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("wb")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>cw</c>"] The quantity that is locked in a trade
        /// </summary>
        [JsonPropertyName("cw")]
        public decimal CrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>bc</c>"] The balance change except PnL and commission
        /// </summary>
        [JsonPropertyName("bc")]
        public decimal BalanceChange { get; set; }
    }

    /// <summary>
    /// Information about an asset position
    /// </summary>
    public record AsterPositionUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pa</c>"] The quantity of the position
        /// </summary>
        [JsonPropertyName("pa")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>ep</c>"] The entry price
        /// </summary>
        [JsonPropertyName("ep")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>bep</c>"] The break even price
        /// </summary>
        [JsonPropertyName("bep")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// ["<c>cr</c>"] The accumulated realized PnL
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>up</c>"] The Unrealized PnL
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>mt</c>"] The margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public MarginType MarginType { get; set; }

        /// <summary>
        /// ["<c>iw</c>"] The isolated wallet (if isolated position)
        /// </summary>
        [JsonPropertyName("iw")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// ["<c>ps</c>"] Position Side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
    }
}
