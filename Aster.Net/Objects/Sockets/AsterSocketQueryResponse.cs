using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Sockets
{
    internal class AsterSocketQueryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
