using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Spot order info
    /// </summary>
    public record AsterSpotOrder
    {
        /// <summary>
         /// ["<c>symbol</c>"] The symbol the order is for
         /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] The order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>orderListId</c>"] Order list id
        /// </summary>
        [JsonPropertyName("orderListId")]
        public long? OrderListId { get; set; }

        /// <summary>
        /// ["<c>origClientOrderId</c>"] Original order id
        /// </summary>
        [JsonPropertyName("origClientOrderId")]
        public string? OriginalClientOrderId { get; set; }

        /// <summary>
        /// ["<c>clientOrderId</c>"] The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>origQty</c>"] The original quantity of the order, as specified in the order parameters by the user
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] The currently executed quantity of the order
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumQuote</c>"] The currently executed amount of quote asset. Amounts to Sum(quantity * price) of executed trades for this order
        /// </summary>
        [JsonPropertyName("cumQuote")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>origQuoteOrderQty</c>"] The original quote order quantity of the order, as specified in the order parameters by the user
        /// </summary>
        [JsonPropertyName("origQuoteOrderQty")]
        public decimal? QuoteQuantity { get; set; }

        /// <summary>
        /// ["<c>status</c>"] The status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>timeInForce</c>"] How long the order is active
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>origType</c>"] The original type of the order
        /// </summary>
        [JsonPropertyName("origType")]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] The stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }        
        /// <summary>
        /// ["<c>time</c>"] The time the order was submitted
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] The time the order was last updated
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// ["<c>avgPrice</c>"] The average price the order was filled
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AverageFillPrice { get; set; }
        /// <summary>
        /// Quantity which is still open to be filled
        /// </summary>
        public decimal QuantityRemaining => Quantity - QuantityFilled;
    }
}
