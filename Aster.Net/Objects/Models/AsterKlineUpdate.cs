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
    /// Aster kline update
    /// </summary>
    public record AsterKlineUpdate : AsterSocketEvent
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("k")]
        public AsterKlineUpdateData Data { get; set; } = default!;
    }

    /// <summary>
    /// The kline data
    /// </summary>
    public record AsterKlineUpdateData
    {
        /// <summary>
        /// The open time of this candlestick
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime CloseTime { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// The symbol this candlestick is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The interval of this candlestick
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// The first trade id in this candlestick
        /// </summary>
        [JsonPropertyName("f")]
        public long FirstTrade { get; set; }
        /// <summary>
        /// The last trade id in this candlestick
        /// </summary>
        [JsonPropertyName("L")]
        public long LastTrade { get; set; }
        /// <summary>
        /// The open price of this candlestick
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The close price of this candlestick
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// The highest price of this candlestick
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price of this candlestick
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The amount of trades in this candlestick
        /// </summary>
        [JsonPropertyName("n")]
        public int TradeCount { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("V")]
        public decimal TakerBuyBaseVolume { get; set; }
        /// <inheritdoc />
        [JsonPropertyName("Q")]
        public decimal TakerBuyQuoteVolume { get; set; }

        /// <summary>
        /// Boolean indicating whether this candlestick is closed
        /// </summary>
        [JsonPropertyName("x")]
        public bool Final { get; set; }
    }
}
