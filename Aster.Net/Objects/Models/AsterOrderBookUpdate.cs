using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record AsterOrderBookUpdate : AsterSocketEvent
    {
        /// <summary>
        /// The symbol of the order book
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// The ID of the first update
        /// </summary>
        [JsonPropertyName("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The ID of the last update Id in last stream
        /// </summary>
        [JsonPropertyName("pu")]
        public long LastUpdateIdStream { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        [JsonPropertyName("b")]
        public AsterOrderBookEntry[] Bids { get; set; } = Array.Empty<AsterOrderBookEntry>();
        [JsonInclude, JsonPropertyName("bids")]
        internal AsterOrderBookEntry[] BidsInt
        {
            set => Bids = value;
        }

        /// <summary>
        /// The list of asks
        /// </summary>
        [JsonPropertyName("a")]
        public AsterOrderBookEntry[] Asks { get; set; } = Array.Empty<AsterOrderBookEntry>();
        [JsonInclude, JsonPropertyName("asks")]
        internal AsterOrderBookEntry[] AsksInt
        {
            set => Asks = value;
        }
    }
}
