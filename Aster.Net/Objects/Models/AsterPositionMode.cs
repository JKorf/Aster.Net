using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record AsterPositionMode
    {
        /// <summary>
        /// ["<c>dualSidePosition</c>"] True: Hedge mode, False: One-way mode
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool DualPositionMode { get; set; }
    }
}
