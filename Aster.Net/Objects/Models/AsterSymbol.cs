using Aster.Net.Enums;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public record AsterSymbol
    {
        /// <summary>
        /// ["<c>filters</c>"] Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public AsterSymbolFilter[] Filters { get; set; } = Array.Empty<AsterSymbolFilter>();
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// ["<c>maintMarginPercent</c>"] The maintenance margin percent
        /// </summary>
        [JsonPropertyName("maintMarginPercent")]
        public decimal MaintMarginPercent { get; set; }
        /// <summary>
        /// ["<c>pricePrecision</c>"] The price Precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>quantityPrecision</c>"] The quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>requiredMarginPercent</c>"] The required margin percentage
        /// </summary>
        [JsonPropertyName("requiredMarginPercent")]
        public decimal RequiredMarginPercent { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginAsset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAsset</c>"] The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quotePrecision</c>"] The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>orderTypes</c>"] Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public OrderType[] OrderTypes { get; set; } = Array.Empty<OrderType>();
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deliveryDate</c>"] Delivery Date
        /// </summary>
        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>onboardDate</c>"] Delivery Date
        /// </summary>
        [JsonPropertyName("onboardDate")]
        public DateTime ListingDate { get; set; }
        /// <summary>
        /// ["<c>triggerProtect</c>"] Trigger protect
        /// </summary>
        [JsonPropertyName("triggerProtect")]
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// ["<c>underlyingType</c>"] Currently Empty
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// ["<c>underlyingSubType</c>"] Sub types
        /// </summary>
        [JsonPropertyName("underlyingSubType")]
        public string[] UnderlyingSubType { get; set; } = Array.Empty<string>();

        /// <summary>
        /// ["<c>liquidationFee</c>"] Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// ["<c>marketTakeBound</c>"] The max price difference rate (from mark price) a market order can make
        /// </summary>
        [JsonPropertyName("marketTakeBound")]
        public decimal MarketTakeBound { get; set; }
        /// <summary>
        /// ["<c>twapMinNotional</c>"] TWAP min notional, the minimum notional value of a TWAP order
        /// </summary>
        [JsonPropertyName("twapMinNotional")]
        public decimal? TwapMinNotional { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Allowed order time in force
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
        /// ["<c>status</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// ["<c>settlePlan</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("settlePlan")]
        public decimal SettlePlan { get; set; }
    }
}
