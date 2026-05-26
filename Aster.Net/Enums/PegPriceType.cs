using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Peg price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PegPriceType>))]
    public enum PegPriceType
    {
        /// <summary>
        /// Counterparty 1
        /// </summary>
        [Map("COUNTERPARTY_1")]
        CounterParty1,
        /// <summary>
        /// Queue 1
        /// </summary>
        [Map("QUEUE_1")]
        Queue1
    }
}
