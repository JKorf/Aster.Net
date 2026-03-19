using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Builder info
    /// </summary>
    public record AsterBuilder
    {
        /// <summary>
        /// User address
        /// </summary>
        [JsonPropertyName("userAddress")]
        public string UserAddress { get; set; } = string.Empty;
        /// <summary>
        /// Builder address
        /// </summary>
        [JsonPropertyName("builderAddress")]
        public string BuilderAddress { get; set; } = string.Empty;
        /// <summary>
        /// Builder name
        /// </summary>
        [JsonPropertyName("builderName")]
        public string BuilderName { get; set; } = string.Empty;
        /// <summary>
        /// Max fee rate
        /// </summary>
        [JsonPropertyName("maxFeeRate")]
        public decimal MaxFeeRate { get; set; }
    }
}
