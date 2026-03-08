using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Multi asset mode config
    /// </summary>
    public record AsterMultiAssetMode
    {
        /// <summary>
        /// ["<c>multiAssetsMargin</c>"] Multi asset mode enabled
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
