using Aster.Net.Converters;
using Aster.Net.Enums;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects
{
    /// <summary>
    /// A filter for order placed on a symbol.
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolFilter>))]
    public record AsterSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        [JsonPropertyName("filterType")]
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolPriceFilter>))]
    public record AsterSymbolPriceFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The minimal price the order can be for
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// The max price the order can be for
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// The tick size of the price. The price can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolPercentPriceFilter>))]
    public record AsterSymbolPercentPriceFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max factor the price can deviate up
        /// </summary>
        public decimal MultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate down
        /// </summary>
        public decimal MultiplierDown { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int? MultiplierDecimal { get; set; }
        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int? AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolPercentPriceBySideFilter>))]
    public record AsterSymbolPercentPriceBySideFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max factor the price can deviate up for buys
        /// </summary>
        public decimal BidMultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate up for sells
        /// </summary>
        public decimal AskMultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate down for buys
        /// </summary>
        public decimal BidMultiplierDown { get; set; }
        /// <summary>
        /// The max factor the price can deviate down for sells
        /// </summary>
        public decimal AskMultiplierDown { get; set; }
        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Lot size filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolLotSizeFilter>))]
    public record AsterSymbolLotSizeFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Market lot size filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMarketLotSizeFilter>))]
    public record AsterSymbolMarketLotSizeFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Min notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMinNotionalFilter>))]
    public record AsterSymbolMinNotionalFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// Whether or not this filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool? ApplyToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int? AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Max notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMaxNotionalFilter>))]
    public record AsterSymbolMaxNotionalFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MaxNotional { get; set; }
    }

    /// <summary>
    /// Notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolNotionalFilter>))]
    public record AsterSymbolNotionalFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// The maximum total quote quantity of an order, This is calculated by Price * Quantity
        /// </summary>
        public decimal? MaxNotional { get; set; }

        /// <summary>
        /// Whether or not the min notional filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool ApplyMinToMarketOrders { get; set; }

        /// <summary>
        /// Whether or not the max notional filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool ApplyMaxToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    ///Max orders filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMaxOrdersFilter>))]
    public record AsterSymbolMaxOrdersFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max number of orders for this symbol
        /// </summary>
        public int MaxNumberOrders { get; set; }
    }

    /// <summary>
    /// Max algo orders filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMaxAlgorithmicOrdersFilter>))]
    public record AsterSymbolMaxAlgorithmicOrdersFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max number of Algorithmic orders for this symbol
        /// </summary>
        public int MaxNumberAlgorithmicOrders { get; set; }
    }

    /// <summary>
    /// Max iceberg parts filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolIcebergPartsFilter>))]
    public record AsterSymbolIcebergPartsFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The max parts of an iceberg order for this symbol.
        /// </summary>
        public int Limit { get; set; }
    }

    /// <summary>
    /// Max position filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolMaxPositionFilter>))]
    public record AsterSymbolMaxPositionFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The MaxPosition filter defines the allowed maximum position an account can have on the base asset of a symbol.
        /// </summary>
        public decimal MaxPosition { get; set; }
    }

    /// <summary>
    /// Trailing delta filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterSymbolTrailingDeltaFilter>))]
    public record AsterSymbolTrailingDeltaFilter : AsterSymbolFilter
    {
        /// <summary>
        /// The MinTrailingAboveDelta filter defines the minimum amount in Basis Point or BIPS above the price to activate the order.
        /// </summary>
        public int MinTrailingAboveDelta { get; set; }
        /// <summary>
        /// The MaxTrailingAboveDelta filter defines the maximum amount in Basis Point or BIPS above the price to activate the order.
        /// </summary>
        public int MaxTrailingAboveDelta { get; set; }
        /// <summary>
        /// The MinTrailingBelowDelta filter defines the minimum amount in Basis Point or BIPS below the price to activate the order.
        /// </summary>
        public int MinTrailingBelowDelta { get; set; }
        /// <summary>
        /// The MaxTrailingBelowDelta filter defines the minimum amount in Basis Point or BIPS below the price to activate the order.
        /// </summary>
        public int MaxTrailingBelowDelta { get; set; }
    }

    /// <summary>
    /// Max Iceberg Orders Filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterMaxNumberOfIcebergOrdersFilter>))]
    public record AsterMaxNumberOfIcebergOrdersFilter : AsterSymbolFilter
    {
        /// <summary>
        /// Maximum number of iceberg orders for this symbol
        /// </summary>
        public int MaxNumIcebergOrders { get; set; }
    }

    /// <summary>
    /// Max Order Amends Filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterMaxNumberOfOrderAmendsFilter>))]
    public record AsterMaxNumberOfOrderAmendsFilter : AsterSymbolFilter
    {
        /// <summary>
        /// Maximum number of order amends for a single order
        /// </summary>
        public int MaxNumOrderAmends { get; set; }
    }

    /// <summary>
    /// Max Order Lists Filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<AsterMaxNumberOfOrderListsFilter>))]
    public record AsterMaxNumberOfOrderListsFilter : AsterSymbolFilter
    {
        /// <summary>
        /// Maximum number of open order lists
        /// </summary>
        public int MaxNumOrderLists { get; set; }
    }
}
