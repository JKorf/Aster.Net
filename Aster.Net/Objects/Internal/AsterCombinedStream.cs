using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Internal
{
    internal record AsterCombinedStream
    {
        /// <summary>
        /// The stream combined
        /// </summary>
        [JsonPropertyName("stream")]
        public string Stream { get; set; } = string.Empty;
    }

    internal record AsterCombinedStream<T> : AsterCombinedStream
    {
        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
