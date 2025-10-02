using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Count down set result
    /// </summary>
    public record AsterCountDownResult
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Count down timer in seconds
        /// </summary>
        [JsonPropertyName("countdownTime")]
        public long CountdownTimer { get; set; }
    }
}
