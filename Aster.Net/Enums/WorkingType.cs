using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Working type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WorkingType>))]
    public enum WorkingType
    {
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// ["<c>CONTRACT_PRICE</c>"] Contract price
        /// </summary>
        [Map("CONTRACT_PRICE")]
        ContractPrice
    }
}
