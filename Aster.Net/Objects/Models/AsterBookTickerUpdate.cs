﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Book ticker update
    /// </summary>
    public record AsterBookTickerUpdate : AsterSocketEvent
    {
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price of the best bid
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Price of the best ask
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
    }
}
