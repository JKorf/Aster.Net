using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Security type
/// </summary>
[JsonConverter(typeof(EnumConverter<SecurityType>))]
public enum SecurityType
{
    /// <summary>
    /// ["<c>USDT_FUTURES</c>"] USDT futures
    /// </summary>
    [Map("USDT_FUTURES")]
    UsdtFutures,
    /// <summary>
    /// ["<c>COIN_FUTURES</c>"] Coin futures
    /// </summary>
    [Map("COIN_FUTURES")]
    CoinFutures,
    /// <summary>
    /// ["<c>OPTIONS</c>"] Options
    /// </summary>
    [Map("OPTIONS")]
    Options,
}
