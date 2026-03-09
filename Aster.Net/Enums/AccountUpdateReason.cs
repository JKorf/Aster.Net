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
        /// ["<c>DEPOSIT</c>"] Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// ["<c>WITHDRAW</c>"] Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// ["<c>ORDER</c>"] Order
        /// </summary>
        [Map("ORDER")]
        Order,
        /// <summary>
        /// ["<c>FUNDING_FEE</c>"] Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// ["<c>WITHDRAW_REJECT</c>"] Withdraw reject
        /// </summary>
        [Map("WITHDRAW_REJECT")]
        WithdrawReject,
        /// <summary>
        /// ["<c>ADJUSTMENT</c>"] Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// ["<c>INSURANCE_CLEAR</c>"] Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// ["<c>ADMIN_DEPOSIT</c>"] Admin deposit
        /// </summary>
        [Map("ADMIN_DEPOSIT")]
        AdminDeposit,
        /// <summary>
        /// ["<c>ADMIN_WITHDRAW</c>"] Admin withdraw
        /// </summary>
        [Map("ADMIN_WITHDRAW")]
        AdminWithdraw,
        /// <summary>
        /// ["<c>MARGIN_TRANSFER</c>"] Margin transfer
        /// </summary>
        [Map("MARGIN_TRANSFER")]
        MarginTransfer,
        /// <summary>
        /// ["<c>MARGIN_TYPE_CHANGE</c>"] Margin type change
        /// </summary>
        [Map("MARGIN_TYPE_CHANGE")]
        MarginTypeChange,
        /// <summary>
        /// ["<c>ASSET_TRANSFER</c>"] Asset transfer
        /// </summary>
        [Map("ASSET_TRANSFER")]
        AssetTransfer,
        /// <summary>
        /// ["<c>OPTIONS_PREMIUM_FEE</c>"] Options premium fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// ["<c>OPTIONS_SETTLE_PROFIT</c>"] Options settle profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// ["<c>AUTO_EXCHANGE</c>"] Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
    }
}
