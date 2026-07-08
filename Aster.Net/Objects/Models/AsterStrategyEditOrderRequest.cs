using Aster.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Strategy order edit request
    /// </summary>
    public record AsterStrategyEditOrderRequest
    {
        /// <summary>
        /// ["<c>strategySubId</c>"] Strategy sub id, needs to be the index in the array starting from 1
        /// </summary>
        [JsonPropertyName("strategySubId")]
        public string StrategySubId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>securityType</c>"] Type of security
        /// </summary>
        [JsonPropertyName("securityType")]
        public SecurityType SecurityType { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>workingType</c>"] Working type
        /// </summary>
        [JsonPropertyName("workingType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public WorkingType? WorkingType { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>closePosition</c>"] Close position
        /// </summary>
        [JsonPropertyName("closePosition"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ClosePosition { get; set; }
        /// <summary>
        /// ["<c>priceProtect</c>"] Price protect
        /// </summary>
        [JsonPropertyName("priceProtect"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? PriceProtect { get; set; }
        /// <summary>
        /// ["<c>activationPrice</c>"] Activation price
        /// </summary>
        [JsonPropertyName("activationPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// ["<c>callbackRate</c>"] Callback rate
        /// </summary>
        [JsonPropertyName("callbackRate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? CallbackRate { get; set; }

    }
}
