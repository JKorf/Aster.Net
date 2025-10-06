﻿using Aster.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record AsterOrderUpdate : AsterSocketEvent
    {
        /// <summary>
        /// Update data
        /// </summary>
        [JsonPropertyName("o")]
        public AsterOrderUpdateData UpdateData { get; set; } = default!;

        /// <summary>
        /// Transaction time
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
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType Type { get; set; }
        /// <summary>
        /// The timespan the order is active
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The stop price of the order
        /// </summary>
        [JsonPropertyName("sp")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The execution type
        /// </summary>
        [JsonPropertyName("x")]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal AccumulatedQuantityOfFilledTrades { get; set; }
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// The fee paid
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee was taken from
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// Bid Notional
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// Ask Notional
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Is this reduce only
        /// </summary>
        [JsonPropertyName("R")]
        public bool IsReduce { get; set; }
        /// <summary>
        /// Stop price working type
        /// </summary>
        [JsonPropertyName("wt")]
        public WorkingType StopPriceWorking { get; set; }
        /// <summary>
        /// Original Order Type
        /// </summary>
        [JsonPropertyName("ot")]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// If Close-All, only pushed with conditional order
        /// </summary>
        [JsonPropertyName("cp")]
        public bool IsClosePositionOrder { get; set; }
        /// <summary>
        /// Activation Price, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("AP")]
        public decimal ActivationPrice { get; set; }
        /// <summary>
        /// Callback Rate, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// Realized profit of the trade
        /// </summary>
        [JsonPropertyName("rp")]
        public decimal RealizedProfit { get; set; }
    }
}
