using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Mini price ticker update
    /// </summary>
    public record AsterMiniTickUpdate : AsterSocketEvent
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Todays open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Total traded volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Total traded quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
    }
}
