using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Mark price info
    /// </summary>
    public record AsterMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current market price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The current index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        [JsonPropertyName("lastFundingRate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Estimated settle price
        /// </summary>
        [JsonPropertyName("estimatedSettlePrice")]
        public decimal? EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
