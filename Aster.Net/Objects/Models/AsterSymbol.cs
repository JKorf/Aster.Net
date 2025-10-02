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
    public record AsterSymbol
    {
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public AsterSymbolFilter[] Filters { get; set; } = Array.Empty<AsterSymbolFilter>();
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// The maintenance margin percent
        /// </summary>
        [JsonPropertyName("maintMarginPercent")]
        public decimal MaintMarginPercent { get; set; }
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
        /// The required margin percentage
        /// </summary>
        [JsonPropertyName("requiredMarginPercent")]
        public decimal RequiredMarginPercent { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
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
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonPropertyName("onboardDate")]
        public DateTime ListingDate { get; set; }
        /// <summary>
        /// Trigger protect
        /// </summary>
        [JsonPropertyName("triggerProtect")]
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// Currently Empty
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Sub types
        /// </summary>
        [JsonPropertyName("underlyingSubType")]
        public string[] UnderlyingSubType { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// The max price difference rate (from mark price) a market order can make
        /// </summary>
        [JsonPropertyName("marketTakeBound")]
        public decimal MarketTakeBound { get; set; }

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
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public AsterSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<AsterSymbolMinNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("settlePlan")]
        public decimal SettlePlan { get; set; }
    }
}
