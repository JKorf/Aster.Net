using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Aggregated trades
    /// </summary>
    public record AsterAggregateTrade
    {
        /// <summary>
        /// ["<c>a</c>"] The id of this aggregation
        /// </summary>
        [JsonPropertyName("a")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>p</c>"] The price of trades in this aggregation
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The total quantity of trades in the aggregation
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>f</c>"] The first trade id in this aggregation
        /// </summary>
        [JsonPropertyName("f")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The last trade id in this aggregation
        /// </summary>
        [JsonPropertyName("l")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>T</c>"] The timestamp of the trades
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
