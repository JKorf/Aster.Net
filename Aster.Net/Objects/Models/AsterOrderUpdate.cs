using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record AsterOrderUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>o</c>"] Update data
        /// </summary>
        [JsonPropertyName("o")]
        public AsterOrderUpdateData UpdateData { get; set; } = default!;

        /// <summary>
        /// ["<c>T</c>"] Transaction time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Update data about an order
    /// </summary>
    public record AsterOrderUpdateData
    {
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
        /// ["<c>ap</c>"] The average price of the order
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>sp</c>"] The stop price of the order
        /// </summary>
        [JsonPropertyName("sp")]
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
        /// ["<c>i</c>"] The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// ["<c>z</c>"] The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal AccumulatedQuantityOfFilledTrades { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal PriceLastFilledTrade { get; set; }
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
        /// ["<c>b</c>"] Bid Notional
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Ask Notional
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// ["<c>R</c>"] Is this reduce only
        /// </summary>
        [JsonPropertyName("R")]
        public bool IsReduce { get; set; }
        /// <summary>
        /// ["<c>wt</c>"] Stop price working type
        /// </summary>
        [JsonPropertyName("wt")]
        public WorkingType StopPriceWorking { get; set; }
        /// <summary>
        /// ["<c>ot</c>"] Original Order Type
        /// </summary>
        [JsonPropertyName("ot")]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// ["<c>ps</c>"] Position side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>cp</c>"] If Close-All, only pushed with conditional order
        /// </summary>
        [JsonPropertyName("cp")]
        public bool IsClosePositionOrder { get; set; }
        /// <summary>
        /// ["<c>AP</c>"] Activation Price, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("AP")]
        public decimal ActivationPrice { get; set; }
        /// <summary>
        /// ["<c>cr</c>"] Callback Rate, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// ["<c>rp</c>"] Realized profit of the trade
        /// </summary>
        [JsonPropertyName("rp")]
        public decimal RealizedProfit { get; set; }
    }
}
