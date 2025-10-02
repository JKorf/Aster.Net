using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Kline/candlestick data
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<AsterKline>))]
    public record AsterKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [ArrayProperty(2)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [ArrayProperty(3)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [ArrayProperty(4)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        [ArrayProperty(5)]
        public decimal Volume { get; set; }
        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        [ArrayProperty(7)]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [ArrayProperty(8)]
        public int TradeCount { get; set; }
        /// <summary>
        /// Taker buy volume in base asset
        /// </summary>
        [ArrayProperty(9)]
        public decimal TakerBuyBaseVolume { get; set; }
        /// <summary>
        /// Taker buy volume in quote asset
        /// </summary>
        [ArrayProperty(10)]
        public decimal TakerBuyQuoteVolume { get; set; }
    }
}
