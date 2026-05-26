using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Quantity unit
/// </summary>
[JsonConverter(typeof(EnumConverter<QuantityUnit>))]
public enum QuantityUnit
{
    /// <summary>
    /// ["<c>BASE</c>"] Quantity in base asset
    /// </summary>
    [Map("BASE")]
    Base,
    /// <summary>
    /// ["<c>QUOTE</c>"] Quantity in quote asset
    /// </summary>
    [Map("QUOTE")]
    Quote,
}
