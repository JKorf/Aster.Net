using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Strategy status
/// </summary>
[JsonConverter(typeof(EnumConverter<StrategyStatus>))]
public enum StrategyStatus
{
    /// <summary>
    /// ["<c>WORKING</c>"] Working
    /// </summary>
    [Map("WORKING")]
    Working,
    /// <summary>
    /// ["<c>EXPIRED</c>"] Expired
    /// </summary>
    [Map("EXPIRED")]
    Expired,
}
