using Aster.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    internal record AsterLiquidationUpdateEvent : AsterSocketEvent
    {
        [JsonPropertyName("o")]
        public AsterLiquidationUpdate Data { get; set; } = default!;
    }

    /// <summary>
    /// Liquidation update
    /// </summary>
    public record AsterLiquidationUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Liquidation Sided
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// Liquidation order type
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType Type { get; set; }

        /// <summary>
        /// Liquidation Time in Force
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Liquidation Original Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Liquidation order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// Liquidation Average Price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// Liquidation Order Status
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Liquidation Last Filled Quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }

        /// <summary>
        /// Liquidation Accumulated fill quantity
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Liquidation Trade Time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
    }
}
