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
        /// ["<c>NEW</c>"] New/unfilled order
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>PARTIALLY_FILLED</c>"] Partially filled order
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>FILLED</c>"] Filled order
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled order
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected order
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired order
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>NEW_INSURANCE</c>"] Insurance fund liquidation
        /// </summary>
        [Map("NEW_INSURANCE")]
        InsuranceFundLiquidation,
        /// <summary>
        /// ["<c>NEW_ADL</c>"] Counter party liquidation
        /// </summary>
        [Map("NEW_ADL")]
        CounterPartyLiquidation
    }
}
