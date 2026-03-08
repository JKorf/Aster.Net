using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Funding configuration
    /// </summary>
    public record AsterFundingInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingFeeCap</c>"] Funding rate cap
        /// </summary>
        [JsonPropertyName("fundingFeeCap")]
        public decimal? FundingFeeCap { get; set; }
        /// <summary>
        /// ["<c>fundingFeeFloor</c>"] Funding rate floor
        /// </summary>
        [JsonPropertyName("fundingFeeFloor")]
        public decimal? FundingFeeFloor { get; set; }
        /// <summary>
        /// ["<c>fundingIntervalHours</c>"] Funding interval in hours
        /// </summary>
        [JsonPropertyName("fundingIntervalHours")]
        public int? FundingIntervalHours { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>interestRate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
    }
}
