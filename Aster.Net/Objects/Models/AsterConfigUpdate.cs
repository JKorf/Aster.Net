using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Config update
    /// </summary>
    public record AsterConfigUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>ac</c>"] Leverage Update data
        /// </summary>
        [JsonPropertyName("ac")]
        public AsterLeverageUpdateData? LeverageUpdateData { get; set; }

        /// <summary>
        /// ["<c>ai</c>"] Position mode Update data
        /// </summary>
        [JsonPropertyName("ai")]
        public AsterConfigUpdateData? ConfigUpdateData { get; set; }

        /// <summary>
        /// ["<c>T</c>"] Transaction time
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
        /// ["<c>s</c>"] The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// ["<c>l</c>"] The symbol this leverage is for
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
        /// ["<c>j</c>"] Multi-Assets Mode
        /// </summary>
        [JsonPropertyName("j")]
        public bool MultiAssetMode { get; set; }
    }
}
