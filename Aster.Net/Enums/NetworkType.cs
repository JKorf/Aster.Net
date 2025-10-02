using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Network type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NetworkType>))]
    public enum NetworkType
    {
        /// <summary>
        /// Ethereum
        /// </summary>
        [Map("1")]
        Ethereum,
        /// <summary>
        /// Binance chain
        /// </summary>
        [Map("56")]
        BSC,
        /// <summary>
        /// Arbitrum
        /// </summary>
        [Map("42161")]
        Arbi,
    }
}
