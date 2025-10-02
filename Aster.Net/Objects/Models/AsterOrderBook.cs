using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public record AsterOrderBook
    {
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        [JsonPropertyName("bids")]
        public AsterOrderBookEntry[] Bids { get; set; } = Array.Empty<AsterOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        [JsonPropertyName("asks")]
        public AsterOrderBookEntry[] Asks { get; set; } = Array.Empty<AsterOrderBookEntry>();

        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<AsterOrderBookEntry>))]
    public record AsterOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price of this order book entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of this price in the order book
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
