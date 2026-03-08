using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// User fee rate
    /// </summary>
    public record AsterUserCommission
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerCommissionRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerCommissionRate")]
        public decimal MakerRate { get; set; }
        /// <summary>
        /// ["<c>takerCommissionRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerCommissionRate")]
        public decimal TakerRate { get; set; }
    }
}
