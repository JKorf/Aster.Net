using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Leverage info
    /// </summary>
    public record AsterLeverage
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional value
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
    }
}
