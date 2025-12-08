using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record AsterExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        [JsonPropertyName("rateLimits")]
        public AsterRateLimit[] RateLimits { get; set; } = Array.Empty<AsterRateLimit>();
        /// <summary>
        /// Filters
        /// </summary>
        [JsonPropertyName("exchangeFilters")]
        public object[] ExchangeFilters { get; set; } = Array.Empty<object>();
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public AsterSymbol[] Symbols { get; set; } = Array.Empty<AsterSymbol>();

        /// <summary>
        /// All assets
        /// </summary>
        [JsonPropertyName("assets")]
        public AsterAsset[] Assets { get; set; } = Array.Empty<AsterAsset>();
    }
}

