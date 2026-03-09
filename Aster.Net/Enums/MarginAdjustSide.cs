using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Margin adjust side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginAdjustSide>))]
    public enum MarginAdjustSide
    {
        /// <summary>
        /// ["<c>1</c>"] Add
        /// </summary>
        [Map("1")]
        Add,
        /// <summary>
        /// ["<c>2</c>"] Remove
        /// </summary>
        [Map("2")]
        Remove
    }
}
