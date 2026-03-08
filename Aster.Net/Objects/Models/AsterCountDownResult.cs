using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Count down set result
    /// </summary>
    public record AsterCountDownResult
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>countdownTime</c>"] Count down timer in seconds
        /// </summary>
        [JsonPropertyName("countdownTime")]
        public long CountdownTimer { get; set; }
    }
}
