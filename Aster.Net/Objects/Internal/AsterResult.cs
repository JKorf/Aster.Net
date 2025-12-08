using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Internal
{
    internal record AsterResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
