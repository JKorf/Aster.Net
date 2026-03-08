using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Recent trade
    /// </summary>
    public record AsterRecentTrade
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity in base asset
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal BaseQuantity { get; set; } 
        /// <summary>
        /// ["<c>quoteQty</c>"] Quantity in quote asset
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the trade
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>isBuyerMaker</c>"] Whether the buyer is maker
        /// </summary>
        [JsonPropertyName("isBuyerMaker")]
        public bool BuyerIsMaker { get; set; }
    }
}
