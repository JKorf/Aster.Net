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
        /// Coin
        /// </summary>
        [Map("COIN")]
        Coin,
        /// <summary>
        /// Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// Pre-market
        /// </summary>
        [Map("PREMARKET")]
        PreMarket
    }
}
