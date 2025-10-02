using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Sockets
{
    internal class AsterSocketMessage
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    internal class AsterSocketRequest : AsterSocketMessage
    {
        [JsonPropertyName("params")]
        public string[] Params { get; set; } = Array.Empty<string>();
    }

    internal class AsterSocketQuery : AsterSocketMessage
    {
        [JsonPropertyName("params")]
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
