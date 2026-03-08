using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Recent trade
    /// </summary>
    public record AsterSpotRecentTrade
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
        // NOTE there seems to be an error in the API where qty is the BaseQuantity and baseQty is an incorrect value
        ///// <summary>
        ///// Quantity in quote asset
        ///// </summary>
        //[JsonPropertyName("qty")]
        //public decimal QuoteQuantity { get; set; }
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
