using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// The time the order will be active for
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// GoodTillCanceled orders will stay active until they are filled or canceled
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// GoodTillCrossing orders will post only
        /// </summary>
        [Map("GTX")]
        GoodTillCrossing,
        /// <summary>
        /// Good til the order expires or is canceled
        /// </summary>
        [Map("GTE_GTC")]
        GoodTillExpiredOrCanceled,
        /// <summary>
        /// RPI
        /// </summary>
        [Map("RPI")]
        Rpi,
        /// <summary>
        /// Hidden
        /// </summary>
        [Map("HIDDEN")]
        Hidden
    }
}
