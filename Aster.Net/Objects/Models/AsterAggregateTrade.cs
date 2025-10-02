using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Aggregated trades
    /// </summary>
    public record AsterAggregateTrade
    {
        /// <summary>
        /// The id of this aggregation
        /// </summary>
        [JsonPropertyName("a")]
        public long Id { get; set; }
        /// <summary>
        /// The price of trades in this aggregation
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The total quantity of trades in the aggregation
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The first trade id in this aggregation
        /// </summary>
        [JsonPropertyName("f")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id in this aggregation
        /// </summary>
        [JsonPropertyName("l")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// The timestamp of the trades
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
