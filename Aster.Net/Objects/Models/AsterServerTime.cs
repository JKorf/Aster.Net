using System;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models
{
    internal record AsterServerTime
    {
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
    }
}
