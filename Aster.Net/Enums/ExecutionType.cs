using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Execution type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExecutionType>))]
    public enum ExecutionType
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>CALCULATED</c>"] Liquidation
        /// </summary>
        [Map("CALCULATED")]
        Liquidation,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>Trade</c>"] Trade
        /// </summary>
        [Map("Trade")]
        Trade
    }
}
