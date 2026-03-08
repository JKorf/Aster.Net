using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Funding rate history
    /// </summary>
    public record AsterFundingRateHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRate</c>"] The finding rate for the given symbol and time
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>fundingTime</c>"] The time the funding rate is applied
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime FundingTime { get; set; }
    }
}
