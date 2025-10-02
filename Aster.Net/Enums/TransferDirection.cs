using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Transfer direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferDirection>))]
    public enum TransferDirection
    {
        /// <summary>
        /// Futures to spot
        /// </summary>
        [Map("FUTURE_SPOT")]
        FuturesToSpot,
        /// <summary>
        /// Spot to futures
        /// </summary>
        [Map("SPOT_FUTURE")]
        SpotToFutures
    }
}
