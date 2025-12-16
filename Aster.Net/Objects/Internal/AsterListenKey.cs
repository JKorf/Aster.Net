using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Internal
{
    internal record AsterListenKey
    {
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
