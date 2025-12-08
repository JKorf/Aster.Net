using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Order type for a futures order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Limit orders will be placed at a specific price. If the price isn't available in the order book for that asset the order will be added in the order book for someone to fill.
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Market order will be placed without a price. The order will be executed at the best price available at that time in the order book.
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// Stop order. Execute a limit order when price reaches a specific Stop price
        /// </summary>
        [Map("STOP")]
        Stop,
        /// <summary>
        /// Stop market order. Execute a market order when price reaches a specific Stop price
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// Take profit market order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
        /// <summary>
        /// A trailing stop order will execute an order when the price drops below a certain percentage from its all time high since the order was activated
        /// </summary>
        [Map("TRAILING_STOP_MARKET")]
        TrailingStopMarket,
        /// <summary>
        /// A liquidation order
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation
    }
}
