using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Symbol ADL quantile estimation
    /// </summary>
    public record AsterQuantileEstimation
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantile
        /// </summary>
        [JsonPropertyName("adlQuantile")]
        public AsterAdlQuantile? AdlQuantile { get; set; }
    }

    /// <summary>
    /// ADL quantile
    /// </summary>
    public record AsterAdlQuantile
    {
        /// <summary>
        /// Long position
        /// </summary>
        [JsonPropertyName("LONG")]
        public int Long { get; set; }
        /// <summary>
        /// Short position
        /// </summary>
        [JsonPropertyName("SHORT")]
        public int Short { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        [JsonPropertyName("HEDGE")]
        public int Hedge { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        [JsonPropertyName("BOTH")]
        public int Both { get; set; }
    }
}
