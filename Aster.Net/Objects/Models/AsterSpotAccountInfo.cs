using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    public record AsterSpotAccountInfo
    {
        /// <summary>
        /// ["<c>feeTier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>canBurnAsset</c>"] Can burn asset
        /// </summary>
        [JsonPropertyName("canBurnAsset")]
        public bool CanBurnAsset { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>balances</c>"] Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public AsterSpotBalance[] Balances { get; set; } = Array.Empty<AsterSpotBalance>();
    }

    /// <summary>
    /// Asset information
    /// </summary>
    public record AsterSpotBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>free</c>"] Free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }       
    }    
}
