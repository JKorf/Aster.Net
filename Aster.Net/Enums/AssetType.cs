using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Asset type
/// </summary>
[JsonConverter(typeof(EnumConverter<AssetType>))]
public enum AssetType
{
    /// <summary>
    /// ["<c>crypto</c>"] Crypto
    /// </summary>
    [Map("crypto")]
    Crypto,
    /// <summary>
    /// ["<c>stable_coin</c>"] Stable coin
    /// </summary>
    [Map("stable_coin")]
    StableCoin,
    /// <summary>
    /// ["<c>stock</c>"] Stock
    /// </summary>
    [Map("stock")]
    Stock,
    /// <summary>
    /// ["<c>other</c>"] Other
    /// </summary>
    [Map("other")]
    Other,
}
