using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Withdraw fee
    /// </summary>
    public record AsterWithdrawFee
    {
        /// <summary>
        /// Token price
        /// </summary>
        [JsonPropertyName("tokenPrice")]
        public decimal TokenPrice { get; set; }
        /// <summary>
        /// Gas cost
        /// </summary>
        [JsonPropertyName("gasCost")]
        public decimal GasCost { get; set; }
        /// <summary>
        /// USD gas value
        /// </summary>
        [JsonPropertyName("gasUsdValue")]
        public decimal GasUsdValue { get; set; }
    }
}
