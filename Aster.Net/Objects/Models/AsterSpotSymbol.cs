using Aster.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public record AsterSpotSymbol
    {
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public AsterSymbolFilter[] Filters { get; set; } = Array.Empty<AsterSymbolFilter>();
        /// <summary>
        /// The price Precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public OrderType[] OrderTypes { get; set; } = Array.Empty<OrderType>();
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// OCO order allowed
        /// </summary>
        [JsonPropertyName("ocoAllowed")]
        public bool OcoAllowed { get; set; }
        /// <summary>
        /// Allowed order time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce[] TimeInForce { get; set; } = Array.Empty<TimeInForce>();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public AsterSymbolPriceFilter? PriceFilter => Filters.OfType<AsterSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public AsterSymbolLotSizeFilter? LotSizeFilter => Filters.OfType<AsterSymbolLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol, specifically for market orders
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMarketLotSizeFilter? MarketLotSizeFilter => Filters.OfType<AsterSymbolMarketLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMaxOrdersFilter? MaxOrdersFilter => Filters.OfType<AsterSymbolMaxOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMaxAlgorithmicOrdersFilter? MaxAlgoOrdersFilter => Filters.OfType<AsterSymbolMaxAlgorithmicOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public AsterSymbolPercentPriceFilter? PricePercentFilter => Filters.OfType<AsterSymbolPercentPriceFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the min notional value
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<AsterSymbolMinNotionalFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the max notional value
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMaxNotionalFilter? MaxNotionalFilter => Filters.OfType<AsterSymbolMaxNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
    }
}
