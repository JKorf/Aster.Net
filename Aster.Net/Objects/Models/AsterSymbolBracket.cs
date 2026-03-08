using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Symbol leverage bracket
    /// </summary>
    public record AsterSymbolBracket
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol or pair
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>brackets</c>"] Brackets
        /// </summary>
        [JsonPropertyName("brackets")]
        public AssetLeverageBracket[] Brackets { get; set; } = Array.Empty<AssetLeverageBracket>();
    }

    /// <summary>
    /// Leverage bracket
    /// </summary>
    public record AssetLeverageBracket
    {
        /// <summary>
        /// ["<c>bracket</c>"] Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }

        /// <summary>
        /// ["<c>initialLeverage</c>"] Max initial leverage for this bracket
        /// </summary>
        [JsonPropertyName("initialLeverage")]
        public int InitialLeverage { get; set; }

        /// <summary>
        /// ["<c>notionalCap</c>"] Cap of this bracket
        /// </summary>
        [JsonPropertyName("notionalCap")]
        public long Cap { get; set; }

        /// <summary>
        /// ["<c>notionalFloor</c>"] Floor of this bracket
        /// </summary>
        [JsonPropertyName("notionalFloor")]
        public long Floor { get; set; }

        /// <summary>
        /// ["<c>maintMarginRatio</c>"] Maintenance ratio for this bracket
        /// </summary>
        [JsonPropertyName("maintMarginRatio")]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// ["<c>cum</c>"] Auxiliary number for quick calculation 
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal MaintAmount { get; set; }
    }
}
