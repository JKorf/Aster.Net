﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record AsterAsset
    {
        /// <summary>
        /// Name of the asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        [JsonPropertyName("marginAvailable")]
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// Auto-exchange threshold in Multi-Assets margin mode
        /// </summary>
        [JsonPropertyName("autoAssetExchange")]
        public decimal? AutoAssetExchange { get; set; }
    }
}
