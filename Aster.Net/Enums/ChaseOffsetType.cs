using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Chase order offset type
/// </summary>
[JsonConverter(typeof(EnumConverter<ChaseOffsetType>))]
public enum ChaseOffsetType
{
    /// <summary>
    /// ["<c>ABSOLUTE</c>"] Absolute offset
    /// </summary>
    [Map("ABSOLUTE")]
    Absolute,
    /// <summary>
    /// ["<c>PERCENTAGE</c>"] Percentage offset
    /// </summary>
    [Map("PERCENTAGE")]
    Percentage,
}
