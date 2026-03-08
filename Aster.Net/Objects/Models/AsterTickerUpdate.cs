using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Price ticker update
    /// </summary>
    public record AsterTickerUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol this data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] The price change of this symbol
        /// </summary>
        [JsonPropertyName("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>P</c>"] The price change percentage of this symbol
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PriceChangePercent { get; set; }
        /// <summary>
        /// ["<c>w</c>"] The weighted average
        /// </summary>
        [JsonPropertyName("w")]
        public decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>x</c>"] The close price of the previous day
        /// </summary>
        [JsonPropertyName("x")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] The most recent trade quantity
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal LastQuantity { get; set; }        
        /// <summary>
        /// ["<c>o</c>"] Todays open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Total traded volume in the base asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Total traded volume in the quote asset
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>F</c>"] The first trade id of today
        /// </summary>
        [JsonPropertyName("F")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The last trade id of today
        /// </summary>
        [JsonPropertyName("L")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>n</c>"] The total trades of id
        /// </summary>
        [JsonPropertyName("n")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// ["<c>O</c>"] The open time of these stats
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>C</c>"] The close time of these stats
        /// </summary>
        [JsonPropertyName("C")]
        public DateTime CloseTime { get; set; }
    }
}
