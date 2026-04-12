using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Agent info
    /// </summary>
    public record AsterAgent
    {
        /// <summary>
        /// ["<c>agentAddress</c>"] Agent address
        /// </summary>
        [JsonPropertyName("agentAddress")]
        public string AgentAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>agentName</c>"] Agent name
        /// </summary>
        [JsonPropertyName("agentName")]
        public string AgentName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ipWhitelist</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhitelist")]
        public string IpWhitelist { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>expired</c>"] Expire time
        /// </summary>
        [JsonPropertyName("expired")]
        public DateTime Expires { get; set; }
        /// <summary>
        /// ["<c>source</c>"] Source
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>canRead</c>"] Read permission
        /// </summary>
        [JsonPropertyName("canRead")]
        public bool CanRead { get; set; }
        /// <summary>
        /// ["<c>canSpotTrade</c>"] Spot trade permission
        /// </summary>
        [JsonPropertyName("canSpotTrade")]
        public bool CanSpotTrade { get; set; }
        /// <summary>
        /// ["<c>canPerpTrade</c>"] Perp trade permission
        /// </summary>
        [JsonPropertyName("canPerpTrade")]
        public bool CanPerpTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Withdraw permission
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
    }
}
