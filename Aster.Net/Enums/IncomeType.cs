using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Income type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IncomeType>))]
    public enum IncomeType
    {
        /// <summary>
        /// ["<c>TRANSFER</c>"] Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// ["<c>WELCOME_BONUS</c>"] Welcome bonus
        /// </summary>
        [Map("WELCOME_BONUS")]
        WelcomeBonus,
        /// <summary>
        /// ["<c>REALIZED_PNL</c>"] Realized profit and loss
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// ["<c>FUNDING_FEE</c>"] Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// ["<c>COMMISSION</c>"] Commission
        /// </summary>
        [Map("COMMISSION")]
        Commission,
        /// <summary>
        /// ["<c>INSURANCE_CLEAR</c>"] Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// ["<c>MARKET_MERCHANT_RETURN_REWARD</c>"] Market merchant return reward
        /// </summary>
        [Map("MARKET_MERCHANT_RETURN_REWARD")]
        MarketMerchantReturnReward,
        /// <summary>
        /// ["<c>TRANSFER_FUTURE_TO_SPOT</c>"] Transfer future to spot account
        /// </summary>
        [Map("TRANSFER_FUTURE_TO_SPOT")]
        TransferFutureToSpotAccount,
        /// <summary>
        /// ["<c>TRANSFER_SPOT_TO_FUTURE</c>"] Transfer spot to future account
        /// </summary>
        [Map("TRANSFER_SPOT_TO_FUTURE")]
        TransfereSpotToFuturesAccount,
    }
}
