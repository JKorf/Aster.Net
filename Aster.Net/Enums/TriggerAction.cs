using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerAction>))]
    public enum TriggerAction
    {
        /// <summary>
        /// ["<c>PLACE_ORDER</c>"] Place order
        /// </summary>
        [Map("PLACE_ORDER")]
        PlaceOrder,
        /// <summary>
        /// ["<c>CANCEL_ORDER</c>"] Cancel order
        /// </summary>
        [Map("CANCEL_ORDER")]
        CancelOrder
    }
}
