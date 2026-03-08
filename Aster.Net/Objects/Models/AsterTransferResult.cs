using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Transfer result
    /// </summary>
    public record AsterTransferResult
    {
        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
