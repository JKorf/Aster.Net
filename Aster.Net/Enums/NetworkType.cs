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
        /// ["<c>1</c>"] Ethereum
        /// </summary>
        [Map("1")]
        Ethereum,
        /// <summary>
        /// ["<c>56</c>"] Binance chain
        /// </summary>
        [Map("56")]
        BSC,
        /// <summary>
        /// ["<c>42161</c>"] Arbitrum
        /// </summary>
        [Map("42161")]
        Arbi,
    }
}
