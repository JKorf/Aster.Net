using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record AsterOrderBookUpdate : AsterSocketEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol of the order book
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>T</c>"] The time the event happened
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// ["<c>U</c>"] The ID of the first update
        /// </summary>
        [JsonPropertyName("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// ["<c>u</c>"] The ID of the last update
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// ["<c>pu</c>"] The ID of the last update Id in last stream
        /// </summary>
        [JsonPropertyName("pu")]
        public long LastUpdateIdStream { get; set; }

        /// <summary>
        /// ["<c>b</c>"] The list of bids
        /// </summary>
        [JsonPropertyName("b")]
        public AsterOrderBookEntry[] Bids { get; set; } = Array.Empty<AsterOrderBookEntry>();
        [JsonInclude, JsonPropertyName("bids")]
        internal AsterOrderBookEntry[] BidsInt
        {
            set => Bids = value;
        }

        /// <summary>
        /// ["<c>a</c>"] The list of asks
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
