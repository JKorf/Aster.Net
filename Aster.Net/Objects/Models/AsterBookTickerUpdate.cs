using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Book ticker update
    /// </summary>
    public record AsterBookTickerUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>u</c>"] Update id
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Price of the best bid
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Quantity of the best bid
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Price of the best ask
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Quantity of the best ask
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
    }
}
