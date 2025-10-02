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
        /// Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// Welcome bonus
        /// </summary>
        [Map("WELCOME_BONUS")]
        WelcomeBonus,
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// Commission
        /// </summary>
        [Map("COMMISSION")]
        Commission,
        /// <summary>
        /// Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// Market merchant return reward
        /// </summary>
        [Map("MARKET_MERCHANT_RETURN_REWARD")]
        MarketMerchantReturnReward,
        /// <summary>
        /// Transfer future to spot account
        /// </summary>
        [Map("TRANSFER_FUTURE_TO_SPOT")]
        TransferFutureToSpotAccount,
        /// <summary>
        /// Transfer spot to future account
        /// </summary>
        [Map("TRANSFER_SPOT_TO_FUTURE")]
        TransfereSpotToFuturesAccount,
    }
}
