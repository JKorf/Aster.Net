using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Auto close type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoCloseType>))]
    public enum AutoCloseType
    {
        /// <summary>
        /// ["<c>LIQUIDATION</c>"] Liquidation order
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// ["<c>ADL</c>"] Auto deleverage order
        /// </summary>
        [Map("ADL")]
        Adl
    }
}
