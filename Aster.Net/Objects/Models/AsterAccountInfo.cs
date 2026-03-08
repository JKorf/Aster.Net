using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    public record AsterAccountInfo
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
        /// ["<c>totalInitialMargin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("totalInitialMargin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalMaintMargin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("totalMaintMargin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>totalWalletBalance</c>"] Total wallet balance
        /// </summary>
        [JsonPropertyName("totalWalletBalance")]
        public decimal TotalWalletBalance { get; set; }
        /// <summary>
        /// ["<c>totalUnrealizedProfit</c>"] Total unrealized profit
        /// </summary>
        [JsonPropertyName("totalUnrealizedProfit")]
        public decimal TotalUnrealizedProfit { get; set; }
        /// <summary>
        /// ["<c>totalMarginBalance</c>"] Total margin balance
        /// </summary>
        [JsonPropertyName("totalMarginBalance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// ["<c>totalPositionInitialMargin</c>"] Total position initial margin
        /// </summary>
        [JsonPropertyName("totalPositionInitialMargin")]
        public decimal TotalPositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalOpenOrderInitialMargin</c>"] Total open order initial margin
        /// </summary>
        [JsonPropertyName("totalOpenOrderInitialMargin")]
        public decimal TotalOpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>totalCrossWalletBalance</c>"] Total cross wallet balance
        /// </summary>
        [JsonPropertyName("totalCrossWalletBalance")]
        public decimal TotalCrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>totalCrossUnPnl</c>"] Total cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("totalCrossUnPnl")]
        public decimal TotalCrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>assets</c>"] Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public AsterAccountInfoAsset[] Assets { get; set; } = Array.Empty<AsterAccountInfoAsset>();
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public AsterPosition[] Positions { get; set; } = Array.Empty<AsterPosition>();
    }

    /// <summary>
    /// Asset information
    /// </summary>
    public record AsterAccountInfoAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        /// <summary>
        /// ["<c>marginBalance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>positionInitialMargin</c>"] Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>openOrderInitialMargin</c>"] Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>crossWalletBalance</c>"] Cross wallet balance
        /// </summary>
        [JsonPropertyName("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>crossUnPnl</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>marginAvailable</c>"] Whether asset can be used as margin in multi asset margin mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }    
}
