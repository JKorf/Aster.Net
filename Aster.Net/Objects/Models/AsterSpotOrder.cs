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
    /// Spot order info
    /// </summary>
    public record AsterSpotOrder
    {
        /// <summary>
         /// The symbol the order is for
         /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }

        /// <summary>
        /// Original order id
        /// </summary>
        [JsonPropertyName("origClientOrderId")]
        public string? OriginalClientOrderId { get; set; }

        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// The original quantity of the order, as specified in the order parameters by the user
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The currently executed quantity of the order
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The currently executed amount of quote asset. Amounts to Sum(quantity * price) of executed trades for this order
        /// </summary>
        [JsonPropertyName("cumQuote")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// The original quote order quantity of the order, as specified in the order parameters by the user
        /// </summary>
        [JsonPropertyName("origQuoteOrderQty")]
        public decimal? QuoteQuantity { get; set; }

        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// How long the order is active
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// The original type of the order
        /// </summary>
        [JsonPropertyName("origType")]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }        
        /// <summary>
        /// The time the order was submitted
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// The time the order was last updated
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// The average price the order was filled
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AverageFillPrice { get; set; }
        /// <summary>
        /// Quantity which is still open to be filled
        /// </summary>
        public decimal QuantityRemaining => Quantity - QuantityFilled;
    }
}
