using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Aggregated trade update
    /// </summary>
    public record AsterStreamMinimalTrade
    {
        /// <summary>
        /// The price of the trades
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The combined quantity of the trades
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }        
        /// <summary>
        /// The time of the trades
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
    }
}
