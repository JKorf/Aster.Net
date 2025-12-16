using Aster.Net.Enums;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order request
    /// </summary>
    public record AsterOrderRequest
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Reduce-only
        /// </summary>
        [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("newClientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Activation price
        /// </summary>
        [JsonPropertyName("activationPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// Callback rate
        /// </summary>
        [JsonPropertyName("callbackRate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? CallbackRate { get; set; }
        /// <summary>
        /// Working type
        /// </summary>
        [JsonPropertyName("workingType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public WorkingType? WorkingType { get; set; }
        /// <summary>
        /// Price protect
        /// </summary>
        [JsonPropertyName("priceProtect"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PriceProtect { get; set; }
    }
}
