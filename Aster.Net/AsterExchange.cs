using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.SharedApis;
using System.Text.Json.Serialization;
using Aster.Net.Converters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters;

namespace Aster.Net
{
    /// <summary>
    /// Aster exchange information and configuration
    /// </summary>
    public static class AsterExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Aster";

        /// <summary>
        /// Display name
        /// </summary>
        public static string DisplayName => "Aster";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Aster.Net/main/Aster.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.asterdex.com/";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://github.com/asterdex/api-docs/blob/master/README.md"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.DEX;

        internal static JsonSerializerOptions _serializerContext = SerializerOptions.WithConverters(JsonSerializerContextCache.GetOrCreate<AsterSourceGenerationContext>());

        /// <summary>
        /// Aliases for Aster assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases =
            [
                new AssetAlias("USDT", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to an Aster recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            return baseAsset + quoteAsset;
        }

        /// <summary>
        /// Rate limiter configuration for the Aster API
        /// </summary>
        public static AsterRateLimiters RateLimiter { get; } = new AsterRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Aster API
    /// </summary>
    public class AsterRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;
        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal AsterRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            RestIp = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 6000 request weight per minute to /api endpoints
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("fapi"), 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 2400 request weight per minute to /fapi endpoints
            Socket = new RateLimitGate("Spot Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new HostFilter("wss://sstream.asterdex.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 4, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding)) // 5 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new HostFilter("wss://fstream.asterdex.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 9, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding)); // 10 requests per second per path (connection)
           

            RestIp.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestIp.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate RestIp { get; private set; }
        internal IRateLimitGate Socket { get; private set; }

    }
}
