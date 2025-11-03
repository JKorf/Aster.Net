using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Funding configuration
    /// </summary>
    public record AsterFundingInfo
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingFeeCap")]
        public decimal? FundingFeeCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingFeeFloor")]
        public decimal? FundingFeeFloor { get; set; }
        /// <summary>
        /// Funding interval in hours
        /// </summary>
        [JsonPropertyName("fundingIntervalHours")]
        public int? FundingIntervalHours { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
    }
}
