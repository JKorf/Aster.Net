using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Withdraw fee
    /// </summary>
    public record AsterWithdrawFee
    {
        /// <summary>
        /// ["<c>tokenPrice</c>"] Token price
        /// </summary>
        [JsonPropertyName("tokenPrice")]
        public decimal TokenPrice { get; set; }
        /// <summary>
        /// ["<c>gasCost</c>"] Gas cost
        /// </summary>
        [JsonPropertyName("gasCost")]
        public decimal GasCost { get; set; }
        /// <summary>
        /// ["<c>gasUsdValue</c>"] USD gas value
        /// </summary>
        [JsonPropertyName("gasUsdValue")]
        public decimal GasUsdValue { get; set; }
    }
}
