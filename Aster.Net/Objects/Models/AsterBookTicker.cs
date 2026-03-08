using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Book ticker
    /// </summary>
    public record AsterBookTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
