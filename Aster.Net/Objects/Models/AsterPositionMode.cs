using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record AsterPositionMode
    {
        /// <summary>
        /// True: Hedge mode, False: One-way mode
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool DualPositionMode { get; set; }
    }
}
