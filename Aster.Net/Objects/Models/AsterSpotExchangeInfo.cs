using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record AsterSpotExchangeInfo
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
        public AsterSpotSymbol[] Symbols { get; set; } = Array.Empty<AsterSpotSymbol>();

        /// <summary>
        /// All assets
        /// </summary>
        [JsonPropertyName("assets")]
        public AsterSpotAsset[] Assets { get; set; } = Array.Empty<AsterSpotAsset>();
    }

    /// <summary>
    /// Spot Asset
    /// </summary>
    public record AsterSpotAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Name { get; set; } = string.Empty;
    }
}

