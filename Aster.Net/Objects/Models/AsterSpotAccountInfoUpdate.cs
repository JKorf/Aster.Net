using System;
using System.Text.Json.Serialization;

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
        /// ["<c>m</c>"] Update reason
        /// </summary>
        [JsonPropertyName("m")]
        public string UpdateReason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>T</c>"] Update time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Balances
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
        /// ["<c>a</c>"] Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>f</c>"] Free
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Locked
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }       
    }    
}
