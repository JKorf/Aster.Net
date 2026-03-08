using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Mark price update
    /// </summary>
    public record AsterMarkPriceUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] The current market price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>i</c>"] The current index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>r</c>"] The last funding rate
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// ["<c>T</c>"] The time the funding rate is applied
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>P</c>"] Estimated settle price
        /// </summary>
        [JsonPropertyName("P")]
        public decimal? EstimatedSettlePrice { get; set; }
    }
}
