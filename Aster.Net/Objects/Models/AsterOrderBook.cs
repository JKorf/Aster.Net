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
        /// ["<c>s</c>"] The symbol of the order book 
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>lastUpdateId</c>"] The ID of the last update
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// ["<c>bids</c>"] The list of bids
        /// </summary>
        [JsonPropertyName("bids")]
        public AsterOrderBookEntry[] Bids { get; set; } = Array.Empty<AsterOrderBookEntry>();

        /// <summary>
        /// ["<c>asks</c>"] The list of asks
        /// </summary>
        [JsonPropertyName("asks")]
        public AsterOrderBookEntry[] Asks { get; set; } = Array.Empty<AsterOrderBookEntry>();

        /// <summary>
        /// ["<c>E</c>"] The symbol of the order book 
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// ["<c>T</c>"] The ID of the last update
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
