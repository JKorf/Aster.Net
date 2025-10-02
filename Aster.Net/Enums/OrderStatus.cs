using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// New/unfilled order
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Partially filled order
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// Filled order
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Canceled order
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Rejected order
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// Expired order
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Insurance fund liquidation
        /// </summary>
        [Map("NEW_INSURANCE")]
        InsuranceFundLiquidation,
        /// <summary>
        /// Counter party liquidation
        /// </summary>
        [Map("NEW_ADL")]
        CounterPartyLiquidation
    }
}
