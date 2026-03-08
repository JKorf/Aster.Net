using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Asset balance
    /// </summary>
    public record AsterBalance
    {
        /// <summary>
        /// ["<c>accountAlias</c>"] Account alias
        /// </summary>
        [JsonPropertyName("accountAlias")]
        public string AccountAlias { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>asset</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>balance</c>"] The total balance of this asset
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal WalletBalance { get; set; }

        /// <summary>
        /// ["<c>crossWalletBalance</c>"] Crossed wallet balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// ["<c>crossUnPnl</c>"] Unrealized profit of crossed positions
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal? CrossUnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Maximum quantity for transfer out
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// ["<c>marginAvailable</c>"] Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool? MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
