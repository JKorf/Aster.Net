using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Mark price update
    /// </summary>
    public record AsterMarkPriceUpdate : AsterSocketEvent
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current market price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The current index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Estimated settle price
        /// </summary>
        [JsonPropertyName("P")]
        public decimal? EstimatedSettlePrice { get; set; }
    }
}
