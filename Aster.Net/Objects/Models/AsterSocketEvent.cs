using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    /// <summary>
    /// Socket update event
    /// </summary>
    public record AsterSocketEvent
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
    }
}
