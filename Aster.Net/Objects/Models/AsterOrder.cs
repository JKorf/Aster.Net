using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record AsterOrder
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>orderId</c>"] The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// Whether or not this order is a liquidation order
        /// </summary>
        [JsonIgnore]
        public bool IsLiquidationOrder => ClientOrderId?.StartsWith("autoclose-") == true;
        /// <summary>
        /// Whether or not this order is an ADL auto close order
        /// </summary>
        [JsonIgnore]
        public bool IsAdlAutoCloseOrder => ClientOrderId?.StartsWith("adl_autoclose-") == true;
        /// <summary>
        /// Whether or not this order is a delisting/delivery settlement order
        /// </summary>
        [JsonIgnore]
        public bool IsSettlementOrder => ClientOrderId?.StartsWith("delivery_autoclose-") == true;

        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] The average price of the order
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Quantity that has been filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumQty</c>"] Cumulative quantity
        /// </summary>
        [JsonPropertyName("cumQty")]
        public decimal? CumulativeQuantity { get; set; }
        /// <summary>
        /// ["<c>cumQuote</c>"] Cumulative quantity in quote asset
        /// </summary>
        [JsonPropertyName("cumQuote")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] The original quantity of the order
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce Only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// ["<c>closePosition</c>"] If order is for closing a position
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ["<c>status</c>"] The current status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price for the order
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// ["<c>timeInForce</c>"] For what time the order lasts
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// ["<c>type</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// ["<c>origType</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("origType")]
        public OrderType OriginalType { get; set; }

        /// <summary>
        /// ["<c>activatePrice</c>"] Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// ["<c>priceRate</c>"] Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("priceRate")]
        public decimal? CallbackRate { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] The time the order was updated
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// ["<c>time</c>"] The time the order was created
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ["<c>workingType</c>"] The working type
        /// </summary>
        [JsonPropertyName("workingType")]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// ["<c>positionSide</c>"] The position side of the order
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// ["<c>priceProtect</c>"] Price protect
        /// </summary>
        [JsonPropertyName("priceProtect")]
        public bool PriceProtect { get; set; }
    }
}
