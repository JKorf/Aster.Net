using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Spot order update
    /// </summary>
    public record AsterSpotOrderUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>i</c>"] The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>S</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>o</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>ot</c>"] The original type of the order
        /// </summary>
        [JsonPropertyName("ot")]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// ["<c>f</c>"] The timespan the order is active
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>p</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>P</c>"] The stop price of the order
        /// </summary>
        [JsonPropertyName("P")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// ["<c>x</c>"] The execution type
        /// </summary>
        [JsonPropertyName("x")]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// ["<c>X</c>"] The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>z</c>"] The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal LastPriceFilled { get; set; }
        /// <summary>
        /// ["<c>n</c>"] The fee paid
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>N</c>"] The asset the fee was taken from
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>T</c>"] The time of the update
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>t</c>"] The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Time the order was created
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>Z</c>"] Cumulative quantity
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] Quote order quantity
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>Y</c>"] Last quote asset transacted quantity (i.e. LastPrice * LastQuantity)
        /// </summary>
        [JsonPropertyName("Y")]
        public decimal LastQuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Average price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// The listen key for which the update was
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }
}
