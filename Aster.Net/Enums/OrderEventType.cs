using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Order event type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderEventType>))]
    public enum OrderEventType
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>PARTIALLY_FILLED_OR_FILLED</c>"] Partially filled or filled
        /// </summary>
        [Map("PARTIALLY_FILLED_OR_FILLED")]
        PartiallyFilledOrFilled,
        /// <summary>
        /// ["<c>FILLED</c>"] Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>REPLACED</c>"] Replaced
        /// </summary>
        [Map("REPLACED")]
        Replaced,
        /// <summary>
        /// ["<c>STOPPED</c>"] Stopped
        /// </summary>
        [Map("STOPPED")]
        Stopped,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
    }
}
