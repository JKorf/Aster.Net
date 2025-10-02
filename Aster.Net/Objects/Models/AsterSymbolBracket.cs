using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Symbol leverage bracket
    /// </summary>
    public record AsterSymbolBracket
    {
        /// <summary>
        /// Symbol or pair
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Brackets
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
        /// Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }

        /// <summary>
        /// Max initial leverage for this bracket
        /// </summary>
        [JsonPropertyName("initialLeverage")]
        public int InitialLeverage { get; set; }

        /// <summary>
        /// Cap of this bracket
        /// </summary>
        [JsonPropertyName("notionalCap")]
        public long Cap { get; set; }

        /// <summary>
        /// Floor of this bracket
        /// </summary>
        [JsonPropertyName("notionalFloor")]
        public long Floor { get; set; }

        /// <summary>
        /// Maintenance ratio for this bracket
        /// </summary>
        [JsonPropertyName("maintMarginRatio")]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Auxiliary number for quick calculation 
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal MaintAmount { get; set; }
    }
}
