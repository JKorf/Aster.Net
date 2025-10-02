using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Account update reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountUpdateReason>))]
    public enum AccountUpdateReason
    {
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// Order
        /// </summary>
        [Map("ORDER")]
        Order,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// Withdraw reject
        /// </summary>
        [Map("WITHDRAW_REJECT")]
        WithdrawReject,
        /// <summary>
        /// Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// Admin deposit
        /// </summary>
        [Map("ADMIN_DEPOSIT")]
        AdminDeposit,
        /// <summary>
        /// Admin withdraw
        /// </summary>
        [Map("ADMIN_WITHDRAW")]
        AdminWithdraw,
        /// <summary>
        /// Margin transfer
        /// </summary>
        [Map("MARGIN_TRANSFER")]
        MarginTransfer,
        /// <summary>
        /// Margin type change
        /// </summary>
        [Map("MARGIN_TYPE_CHANGE")]
        MarginTypeChange,
        /// <summary>
        /// Asset transfer
        /// </summary>
        [Map("ASSET_TRANSFER")]
        AssetTransfer,
        /// <summary>
        /// Options premium fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// Options settle profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
    }
}
