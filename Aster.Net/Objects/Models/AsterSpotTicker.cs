using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Price ticker info
    /// </summary>
    public record AsterSpotTicker : AsterTicker
    {
        /// <summary>
        /// ["<c>bidPrice</c>"] The current best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] The current best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] The current best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] The current best ask quantity
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal BestAskQuantity { get; set; }
    }
}
