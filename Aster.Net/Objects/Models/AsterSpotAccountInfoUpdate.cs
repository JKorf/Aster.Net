using Aster.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Account info update
    /// </summary>
    public record AsterSpotAccountUpdate : AsterSocketEvent
    {
        /// <summary>
        /// Listen key
        /// </summary>
        [JsonIgnore]
        public string ListenKey { get; set; } = string.Empty;
        /// <summary>
        /// Update reason
        /// </summary>
        [JsonPropertyName("m")]
        public string UpdateReason { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("B")]
        public AsterSpotBalanceUpdate[] Balances { get; set; } = Array.Empty<AsterSpotBalanceUpdate>();
    }

    /// <summary>
    /// Asset information
    /// </summary>
    public record AsterSpotBalanceUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Free
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }       
    }    
}
