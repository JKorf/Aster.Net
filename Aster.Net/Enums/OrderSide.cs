using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// Buy order
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// Sell order
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
