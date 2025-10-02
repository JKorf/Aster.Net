using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Config update
    /// </summary>
    public record AsterConfigUpdate : AsterSocketEvent
    {
        /// <summary>
        /// Leverage Update data
        /// </summary>
        [JsonPropertyName("ac")]
        public AsterLeverageUpdateData? LeverageUpdateData { get; set; }

        /// <summary>
        /// Position mode Update data
        /// </summary>
        [JsonPropertyName("ai")]
        public AsterConfigUpdateData? ConfigUpdateData { get; set; }

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Config update data
    /// </summary>
    public record AsterLeverageUpdateData
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// The symbol this leverage is for
        /// </summary>
        [JsonPropertyName("l")]
        public int Leverage { get; set; }
    }

    /// <summary>
    /// Position mode update data
    /// </summary>
    public record AsterConfigUpdateData
    {
        /// <summary>
        /// Multi-Assets Mode
        /// </summary>
        [JsonPropertyName("j")]
        public bool MultiAssetMode { get; set; }
    }
}
