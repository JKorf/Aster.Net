using Aster.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Income history record
    /// </summary>
    public record AsterIncome
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset</c>"] Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>incomeType</c>"] Income type
        /// </summary>
        [JsonPropertyName("incomeType")]
        public IncomeType Type { get; set; }
        /// <summary>
        /// ["<c>info</c>"] Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>income</c>"] Income
        /// </summary>
        [JsonPropertyName("income")]
        public decimal Income { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>tradeId</c>"] Trade id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
    }
}
