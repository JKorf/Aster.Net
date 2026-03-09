using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Underlying Type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnderlyingType>))]
    public enum UnderlyingType
    {
        /// <summary>
        /// ["<c>COIN</c>"] Coin
        /// </summary>
        [Map("COIN")]
        Coin,
        /// <summary>
        /// ["<c>INDEX</c>"] Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// ["<c>PREMARKET</c>"] Pre-market
        /// </summary>
        [Map("PREMARKET")]
        PreMarket
    }
}
