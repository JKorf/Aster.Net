using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// ["<c>CROSSED</c>"] Cross margin
        /// </summary>
        [Map("CROSSED")]
        Cross,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}
