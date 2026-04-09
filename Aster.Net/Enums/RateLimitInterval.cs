using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Rate limit on what unit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RateLimitInterval>))]
    public enum RateLimitInterval
    {
        /// <summary>
        /// Seconds
        /// </summary>
        [Map("SECOND")]
        Second,
        /// <summary>
        /// Minutes
        /// </summary>
        [Map("MINUTE")]
        Minute,
        /// <summary>
        /// Days
        /// </summary>
        [Map("DAY")]
        Day
    }
}
