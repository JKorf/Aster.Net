using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Price info
    /// </summary>
    public record AsterPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the price is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] The price of the symbol
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? Timestamp { get; set; }
    }
}
